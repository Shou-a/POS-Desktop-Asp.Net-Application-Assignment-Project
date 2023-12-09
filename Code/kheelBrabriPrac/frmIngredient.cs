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
    public partial class frmIngredient : Form
    {
        private frmMaintainIngredient maintain;
        bool isGridInitialized = false;
        DataTable data;
        public frmIngredient()
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
                DataColumn dataColumn_name = new DataColumn("Name", typeof(string));
                DataColumn dataColumn_description = new DataColumn("Description", typeof(string));
                DataColumn dataColumn_unit = new DataColumn("Remaining_Unit", typeof(string));


                dataColumn_id.DefaultValue = 0;
                dataColumn_name.DefaultValue = "Name";
                dataColumn_description.DefaultValue = "Description";
                dataColumn_unit.DefaultValue = "Remaining_Unit";

                data.Columns.Add(dataColumn_id);
                data.Columns.Add(dataColumn_name);
                data.Columns.Add(dataColumn_description);
                data.Columns.Add(dataColumn_unit);


            }
            isGridInitialized = true;

        }
        protected void FillData()
        {
            InitializeGrd();


            data.Rows.Clear();

            IngredientCollection ingredients = new IngredientCollection().Load();

            foreach (var item in ingredients)
            {
                DataRow dr = data.NewRow();

                dr["Id"] = item.Id;
                dr["Name"] = item.Name;
                dr["Description"] = item.Description;
                dr["Remaining_Unit"] = item.Unit;

                data.Rows.Add(dr);
            }



            if (data.Rows.Count > 0)
            {
                grdItems.DataSource = data;

                grdItems.Columns["Name"].DisplayIndex = 0;
                grdItems.Columns["Description"].DisplayIndex = 1;
                grdItems.Columns["Remaining_Unit"].DisplayIndex = 2;
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
                if (maintain != null)
                {
                    maintain.Close();
                }
                int id = int.Parse(grdItems.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                maintain = new frmMaintainIngredient(id);
                maintain.Show();
            }
            //delete
            else if (e.ColumnIndex == 1)
            {
                int id = int.Parse(grdItems.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                ProductIngredientCollection products = new ProductIngredientCollection()
                    .Where(ProductIngredient.Columns.IngredientId, id)
                    .Load();

                if(products.Count > 0)
                {
                    MessageBox.Show("Can't Delete !\nIngredient in Use of an Product.");
                }
                else
                {
                    Ingredient.Delete(id);
                    FillData();
                }

            }
        }
    }
}
