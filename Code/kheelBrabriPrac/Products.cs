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
    public partial class Products : Form
    {
        private MaintainProduct maintain;
        bool isGridInitialized = false;
        DataTable data;
        public Products()
        {
            InitializeComponent();
            FillCategories();
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
                DataColumn dataColumn_Price = new DataColumn("Price", typeof(string));
                DataColumn dataColumn_units = new DataColumn("stored_units", typeof(string));
                DataColumn dataColumn_CatgoryName = new DataColumn("Category_Name", typeof(string));


                dataColumn_id.DefaultValue = 0;
                dataColumn_name.DefaultValue = "Name";
                dataColumn_description.DefaultValue = "Description";
                dataColumn_Price.DefaultValue = "Price";
                dataColumn_units.DefaultValue = "Unit";
                dataColumn_CatgoryName.DefaultValue = "Category_Name";

                data.Columns.Add(dataColumn_id);
                data.Columns.Add(dataColumn_name);
                data.Columns.Add(dataColumn_description);
                data.Columns.Add(dataColumn_Price);
                data.Columns.Add(dataColumn_units);
                data.Columns.Add(dataColumn_CatgoryName);


            }
            isGridInitialized = true;

        }
        protected void FillData()
        {
            InitializeGrd();


            data.Rows.Clear();

            ProductCollection products = new ProductCollection();

            if (cmbCategories.SelectedIndex > 0)
            {
                products.Where(Product.Columns.CategoryId, new ProductCategoryCollection().Where(ProductCategory.Columns.Name, cmbCategories.SelectedItem.ToString()).Load().FirstOrDefault().Id);
            }
            products.Load();

            foreach (var item in products)
            {
                DataRow dr = data.NewRow();

                dr["Id"] = item.Id;
                dr["Name"] = item.Name;
                dr["Description"] = item.Description;
                dr["Price"] = item.Price;
                dr["stored_units"] = item.Unit.HasValue ? item.Unit.Value : 0;
                dr["Category_Name"] = item.ProductCategory.Name;

                data.Rows.Add(dr);
            }



            if (data.Rows.Count > 0)
            {
                grdProducts.DataSource = data;

                grdProducts.Columns["Name"].DisplayIndex = 0;
                grdProducts.Columns["Description"].DisplayIndex = 1;
                grdProducts.Columns["stored_units"].DisplayIndex = 2;
                grdProducts.Columns["Price"].DisplayIndex = 3;
                grdProducts.Columns["Category_Name"].DisplayIndex = 4;
                grdProducts.Columns["Id"].Visible = false;

                grdProducts.Visible = true;

            }
            else
            {
                grdProducts.Visible = false;
            }

        }

        protected void FillCategories()
        {
            ProductCategoryCollection productCategoriesproducts = new ProductCategoryCollection()
                .Load();

            cmbCategories.Items.Insert(0, "All");

            for (int i = 0; i < productCategoriesproducts.Count; i++)
            {
                cmbCategories.Items.Insert(productCategoriesproducts[i].Id, productCategoriesproducts[i].Name);
            }

            cmbCategories.SelectedIndex = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //edit
            if (e.ColumnIndex == 0)
            {
                if (maintain != null)
                {
                    maintain.Close();
                }
                int id = int.Parse(grdProducts.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                maintain = new MaintainProduct(id);
                maintain.Show();
            }
            //delete
            else if (e.ColumnIndex == 1)
            {
                int id = int.Parse(grdProducts.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                try
                {
                    Product.Delete(id);
                    FillData();
                }
                catch (Exception)
                {
                    MessageBox.Show("this Product is inuse Somewere!\nYou Cam't Delete it.");
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductCategoryCollection productCategories = new ProductCategoryCollection().Load();
            IngredientCollection ingredients = new IngredientCollection().Load();


            if (maintain != null)
            {
                maintain.Close();
            }

            if (productCategories.Count <= 0)
            {
                MessageBox.Show("Category Not Added Yet !\nFirst Add category to Add Product.");
            }
            else if (ingredients.Count <= 0)
            {
                MessageBox.Show("Ingredient Not Added Yet !\nFirst Add Ingredient to Add Product.");
            }
            else
            {
                maintain = new MaintainProduct();
                maintain.Show();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillData();
        }
    }
}
