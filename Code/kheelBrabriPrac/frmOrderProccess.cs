using DataAccess;
using kheelBrabriPrac.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kheelBrabriPrac
{
    public partial class frmOrderProccess : Form
    {
        DataTable data;
        bool isGridInitialized = false;
        List<clOrder> Records;

        public frmOrderProccess(List<clOrder> OrderDetails, bool isPreview = false)
        {
            InitializeComponent();

            if (isPreview)
            {
                btnPay.Visible = false;
            }
            InitializeData();
            FillData(OrderDetails);
            updateAllPricing();


            lblDate.Text = "Printed On : " + DateTime.Now.ToString("dd-MMM-yyyy");

            Records = OrderDetails;
        }

        public void InitializeData()
        {
            if (!isGridInitialized)
            {
                data = new DataTable();

                DataColumn dataColumn_id = new DataColumn("Id", typeof(string));
                DataColumn dataColumn_name = new DataColumn("name", typeof(string));
                DataColumn dataColumn_price = new DataColumn("price", typeof(string));
                DataColumn dataColumn_qty = new DataColumn("qty", typeof(string));
                DataColumn dataColumn_dis = new DataColumn("discount", typeof(string));
                DataColumn dataColumn_cost = new DataColumn("cost", typeof(string));


                dataColumn_id.DefaultValue = 0;
                dataColumn_name.DefaultValue = "Name";
                dataColumn_price.DefaultValue = "Price";
                dataColumn_qty.DefaultValue = "Qty";
                dataColumn_dis.DefaultValue = "Discount";
                dataColumn_cost.DefaultValue = "Cost";

                data.Columns.Add(dataColumn_id);
                data.Columns.Add(dataColumn_name);
                data.Columns.Add(dataColumn_price);
                data.Columns.Add(dataColumn_dis);
                data.Columns.Add(dataColumn_qty);
                data.Columns.Add(dataColumn_cost);
            }
            isGridInitialized = true;

        }

        protected void FillData(List<clOrder> OrderDetails)
        {
            foreach (clOrder item in OrderDetails)
            {
                DataRow dr = data.NewRow();

                dr["Id"] = item.product_id;
                dr["name"] = item.product_name;
                dr["price"] = item.each_cost;
                dr["qty"] = item.qty;
                dr["discount"] = item.discount;
                dr["cost"] = item.cost;

                data.Rows.Add(dr);
            }

            grdCart.DataSource = data;
        }
        private void printButton_Click(object sender, EventArgs e)
        {
            SaveRecords();

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }

            this.Close();
        }
        protected void updateAllPricing()
        {
            int total = 0;
            int dis = 0;
            int netTotal = 0;

            foreach (DataGridViewRow row in grdCart.Rows)
            {
                total += (Convert.ToInt32(row.Cells["price"].Value) * Convert.ToInt32(row.Cells["qty"].Value));
                dis += Convert.ToInt32(row.Cells["discount"].Value);
            }

            txtAllTotal.Text = total.ToString();
            txtAllDic.Text = dis.ToString();
            txtAllNetTotal.Text = (total - dis).ToString();

        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;

            Bitmap bitmap = new Bitmap(this.Width, this.Height);

            this.DrawToBitmap(bitmap, new Rectangle(0, 0, this.Width, this.Height));

            graphics.DrawImage(bitmap, new Point(0, 0));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected void SaveRecords()
        {
            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.Save();

            foreach (clOrder item in Records)
            {
                Product product = new Product(item.product_id);

                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductId = item.product_id;
                orderDetail.OrderId = order.Id;
                orderDetail.EachCost = item.each_cost;
                orderDetail.Quantity = item.qty;
                orderDetail.Discount = item.discount;

                orderDetail.Save();

                product.Unit = Convert.ToInt32(product.Unit) - item.qty;
                product.Save();
            }
        }
    }
}
