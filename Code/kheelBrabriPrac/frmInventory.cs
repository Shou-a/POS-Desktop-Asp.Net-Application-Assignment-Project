using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kheelBrabriPrac
{
    public partial class frmInventory : Form
    {
        private frmMaintainInventory maintain;
        bool isGridInitialized = false;
        DataTable data;

        public frmInventory()
        {
            InitializeComponent();
            FillData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductCollection products = new ProductCollection().Load();

            if(products.Count > 0)
            {
                maintain = new frmMaintainInventory();
                maintain.Show();
            }
            else
            {
                MessageBox.Show("First add Any Product to Make Inventory of that !");
            }
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
                DataColumn dataColumn_name = new DataColumn("Product_Name", typeof(string));
                DataColumn dataColumn_qty = new DataColumn("Quantity", typeof(string));
                DataColumn dataColumn_cost = new DataColumn("Cost", typeof(string));


                dataColumn_id.DefaultValue = 0;
                dataColumn_name.DefaultValue = "Product_Name";
                dataColumn_qty.DefaultValue = "Quantity";
                dataColumn_cost.DefaultValue = "Cost";

                data.Columns.Add(dataColumn_id);
                data.Columns.Add(dataColumn_name);
                data.Columns.Add(dataColumn_qty);
                data.Columns.Add(dataColumn_cost);


            }
            isGridInitialized = true;

        }
        protected void FillData()
        {
            InitializeGrd();


            data.Rows.Clear();

            InventoryLogCollection inventories = new InventoryLogCollection().Load();

            foreach (var item in inventories)
            {
                DataRow dr = data.NewRow();

                dr["Id"] = item.Id;
                dr["Product_Name"] = item.Product.Name;
                dr["Quantity"] = item.Quantity;
                dr["Cost"] = item.Cost;

                data.Rows.Add(dr);
            }



            if (data.Rows.Count > 0)
            {
                grdItems.DataSource = data;

                grdItems.Columns["Product_Name"].DisplayIndex = 0;
                grdItems.Columns["Quantity"].DisplayIndex = 1;
                grdItems.Columns["Cost"].DisplayIndex = 2;
                grdItems.Columns["Id"].Visible = false;

                grdItems.Visible = true;
            }
            else
            {
                grdItems.Visible = false;
            }

        }

        private void grdItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int id = int.Parse(grdItems.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                InventoryLog log = new InventoryLog(id);

                if (log.Product.Unit > log.Quantity)
                {
                    Product product = new Product(log.ProductId);
                    product.Unit = product.Unit - log.Quantity;
                    product.Save();

                    InventoryLog.Delete(id);
                    FillData();
                }
                else
                {
                    MessageBox.Show("Can't Delete !\nProduct haven't enough Quantity to Delete.");
                }

            }
        }
    }
}
