using DataAccess;
using System;
using System.Data;
using System.Windows.Forms;
using SubSonic;
using System.Linq;
using System.Collections.Generic;
using kheelBrabriPrac.helpers;

namespace kheelBrabriPrac
{
    public partial class frmOrdersList : Form
    {
        private frmMaintainIngredient maintain;
        bool isGridInitialized = false;
        private frmOrderProccess orderProccess;
        DataTable data;

        public frmOrdersList()
        {
            InitializeComponent();
            FillData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            maintain = new frmMaintainIngredient();
            maintain.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //edit

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillData();
        }
        public void InitializeGrd()
        {
            if (!isGridInitialized)
            {
                data = new DataTable();

                DataColumn dataColumn_id = new DataColumn("Id", typeof(string));
                DataColumn dataColumn_name = new DataColumn("names", typeof(string));
                DataColumn dataColumn_total = new DataColumn("total", typeof(string));
                DataColumn dataColumn_dis = new DataColumn("discount", typeof(string));
                DataColumn dataColumn_nTotal = new DataColumn("net_total", typeof(string));


                dataColumn_id.DefaultValue = 0;
                dataColumn_name.DefaultValue = "Product Names Missed !";
                dataColumn_total.DefaultValue = "Total";
                dataColumn_dis.DefaultValue = "-";
                dataColumn_nTotal.DefaultValue = "Total";

                data.Columns.Add(dataColumn_id);
                data.Columns.Add(dataColumn_name);
                data.Columns.Add(dataColumn_total);
                data.Columns.Add(dataColumn_dis);
                data.Columns.Add(dataColumn_nTotal);


            }
            isGridInitialized = true;

        }
        protected void FillData()
        {
            InitializeGrd();


            data.Rows.Clear();

            OrderCollection orders = new OrderCollection().Load();

            foreach (var order in orders)
            {
                OrderDetailCollection orderDetails = new OrderDetailCollection()
                    .Where(OrderDetail.Columns.OrderId, order.Id)
                    .Load();

                DataRow dr = data.NewRow();

                dr["Id"] = order.Id;
                dr["names"] = string.Join(", ", orderDetails.Select(x => x.Product.Name).ToList());
                dr["total"] = orderDetails.Sum(x => x.EachCost * x.Quantity);
                dr["discount"] = orderDetails.Sum(x => x.Discount);
                dr["net_total"] = orderDetails.Sum(x => x.EachCost * x.Quantity) - orderDetails.Sum(x => x.Discount);

                data.Rows.Add(dr);
            }



            if (data.Rows.Count > 0)
            {
                grdItems.DataSource = data;

                grdItems.Columns["names"].DisplayIndex = 0;
                grdItems.Columns["total"].DisplayIndex = 1;
                grdItems.Columns["discount"].DisplayIndex = 2;
                grdItems.Columns["net_total"].DisplayIndex = 3;
                grdItems.Columns["Id"].Visible = false;

                grdItems.Visible = true;
            }
            else
            {
                grdItems.Visible = false;
            }

        }

        private void grdItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<clOrder> allItems = new List<clOrder>();

            if (orderProccess != null)
            {
                orderProccess.Close();
            }

            int OrderID = Convert.ToInt32(grdItems.Rows[e.RowIndex].Cells["Id"].Value);

            OrderDetailCollection orderDetails = new OrderDetailCollection()
                .Where(OrderDetail.Columns.OrderId, OrderID)
                .Load();

            foreach (OrderDetail _item in orderDetails)
            {
                clOrder item = new clOrder();

                item.product_id = _item.ProductId;
                item.product_name = _item.Product.Name;
                item.each_cost = _item.Product.Price;
                item.qty = _item.Quantity;
                item.discount = _item.Discount.Value;
                item.cost = _item.EachCost;

                allItems.Add(item);
            }
            orderProccess = new frmOrderProccess(allItems, true);
            orderProccess.Show();
        }
    }
}
