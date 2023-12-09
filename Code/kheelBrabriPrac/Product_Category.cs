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
    public partial class Product_Category : Form
    {
        private maintainCategory maintain;
        bool isGridInitialized = false;
        DataTable data;
        public Product_Category()
        {
            InitializeComponent();
            FillData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            maintain = new maintainCategory();
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
                DataColumn dataColumn_name = new DataColumn("Name", typeof(string));
                DataColumn dataColumn_description = new DataColumn("Description", typeof(string));


                dataColumn_id.DefaultValue = 0;
                dataColumn_name.DefaultValue = "Name";
                dataColumn_description.DefaultValue = "Description";

                data.Columns.Add(dataColumn_id);
                data.Columns.Add(dataColumn_name);
                data.Columns.Add(dataColumn_description);


            }
            isGridInitialized = true;

        }

        protected void FillData()
        {
            InitializeGrd();


            data.Rows.Clear();

            ProductCategoryCollection categories = new ProductCategoryCollection().Load();

            foreach (var item in categories)
            {
                DataRow dr = data.NewRow();

                dr["Id"] = item.Id;
                dr["Name"] = item.Name;
                dr["Description"] = item.Description;

                data.Rows.Add(dr);
            }



            if (data.Rows.Count > 0)
            {
                grdProductCategory.DataSource = data;

                grdProductCategory.Columns["Name"].DisplayIndex = 0;
                grdProductCategory.Columns["Description"].DisplayIndex = 1;
                grdProductCategory.Columns["Id"].Visible = false;

                grdProductCategory.Visible = true;
            }
            else
            {
                grdProductCategory.Visible = false;
            }

        }

        private void grdProductCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (maintain != null)
                {
                    maintain.Close();
                }
                int id = int.Parse(grdProductCategory.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                maintain = new maintainCategory(id);
                maintain.Show();
            }
            //delete
            else if (e.ColumnIndex == 1)
            {
                int id = int.Parse(grdProductCategory.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                try
                {
                    ProductCategory.Delete(id);
                    FillData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Category is inuser Somewhere You Can't Delete it !");
                }
            }
        }
    }
}
