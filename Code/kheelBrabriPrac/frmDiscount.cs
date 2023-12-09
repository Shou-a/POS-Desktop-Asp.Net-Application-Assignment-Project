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
    public partial class frmDiscount : Form
    {
        private frmMaintainDiscount maintain;
        bool isGridInitialized = false;
        DataTable data;

        public frmDiscount()
        {
            InitializeComponent();
            FillData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            maintain = new frmMaintainDiscount();
            maintain.Show();
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
                DataColumn dataColumn_name = new DataColumn("name", typeof(string));
                DataColumn dataColumn_des = new DataColumn("des", typeof(string));
                DataColumn dataColumn_value = new DataColumn("value", typeof(string));
                DataColumn dataColumn_from = new DataColumn("from", typeof(string));
                DataColumn dataColumn_to = new DataColumn("to", typeof(string));


                dataColumn_id.DefaultValue = 0;
                dataColumn_name.DefaultValue = "Name";
                dataColumn_des.DefaultValue = "des";
                dataColumn_value.DefaultValue = "value";
                dataColumn_value.DefaultValue = "from";
                dataColumn_value.DefaultValue = "to";

                data.Columns.Add(dataColumn_id);
                data.Columns.Add(dataColumn_name);
                data.Columns.Add(dataColumn_des);
                data.Columns.Add(dataColumn_value);
                data.Columns.Add(dataColumn_from);
                data.Columns.Add(dataColumn_to);


            }
            isGridInitialized = true;

        }
        protected void FillData()
        {
            InitializeGrd();


            data.Rows.Clear();

            DiscountCollection discounts = new DiscountCollection().Load();

            foreach (var item in discounts)
            {
                DataRow dr = data.NewRow();

                dr["Id"] = item.Id;
                dr["name"] = item.Name;
                dr["des"] = item.Description;
                dr["value"] = item.ValueX;
                dr["from"] = Convert.ToDateTime(item.FromDate).ToString("dd-MMM-yyyy");
                dr["to"] = Convert.ToDateTime(item.ToDate).ToString("dd-MMM-yyyy");

                data.Rows.Add(dr);
            }



            if (data.Rows.Count > 0)
            {
                grdItems.DataSource = data;

                grdItems.Columns["name"].DisplayIndex = 0;
                grdItems.Columns["des"].DisplayIndex = 1;
                grdItems.Columns["value"].DisplayIndex = 2;
                grdItems.Columns["from"].DisplayIndex = 3;
                grdItems.Columns["to"].DisplayIndex = 4;
                grdItems.Columns["Id"].Visible = false;

                grdItems.Visible = true;
            }
            else
            {
                grdItems.Visible = false;
            }

        }
        

        private void grdItems_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int id = int.Parse(grdItems.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                if (maintain != null)
                {
                    maintain.Close();
                }
                maintain = new frmMaintainDiscount(id);
                maintain.Show();

            }
            else if(e.ColumnIndex == 1)
            {
                int id = int.Parse(grdItems.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                try
                {
                    Discount.Delete(id);
                }
                catch (Exception)
                {
                    MessageBox.Show("Can't Delete !\nDiscount haven't enough Quantity to Delete.");
                }
            }

            FillData();
        }
    }
}
