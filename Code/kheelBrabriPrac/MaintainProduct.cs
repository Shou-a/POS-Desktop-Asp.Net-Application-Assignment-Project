using System;
using SubSonic;
using DataAccess;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace kheelBrabriPrac
{
    public partial class MaintainProduct : Form
    {
        public int SelectedRecordId { get; set; }
        DataTable Ingredientdata;
        bool isGridInitialized = false;
        List<int> addIngredientIds;
        List<int> remIngredientIds = new List<int>();

        public MaintainProduct(int selectedRecordId = -1)
        {
            InitializeComponent();
            FillCategories();
            FillIngredients();
            InitializeIngredientGrd();
            if (selectedRecordId > 0)
            {
                lblLagend.Text = "Edit Product";
                btnOperation.Text = "Update";

                SelectedRecordId = selectedRecordId;

                FillData();
            }
            else
            {
                lblLagend.Text = "Add Product";
                btnOperation.Text = "Save";
            }
        }

        public void InitializeIngredientGrd()
        {
            if (!isGridInitialized)
            {
                Ingredientdata = new DataTable();

                DataColumn dataColumn_id = new DataColumn("Id", typeof(string));
                DataColumn dataColumn_name = new DataColumn("Name", typeof(string));
                DataColumn dataColumn_value = new DataColumn("Value", typeof(string));


                dataColumn_id.DefaultValue = 0;
                dataColumn_name.DefaultValue = "Name";
                dataColumn_value.DefaultValue = "Value";

                Ingredientdata.Columns.Add(dataColumn_id);
                Ingredientdata.Columns.Add(dataColumn_name);
                Ingredientdata.Columns.Add(dataColumn_value);


            }
            isGridInitialized = true;

        }

        protected void AddIngredient()
        {
            InitializeIngredientGrd();

            IngredientCollection ingredients = new IngredientCollection()
                .Where(Ingredient.Columns.Name, cmbIngrediants.SelectedItem.ToString())
                .Load();


            DataRow dr = Ingredientdata.NewRow();

            dr["Id"] = ingredients.FirstOrDefault().Id;
            dr["Name"] = ingredients.FirstOrDefault().Name;
            dr["Value"] = txtUnit.Text;

            Ingredientdata.Rows.Add(dr);



            if (Ingredientdata.Rows.Count > 0)
            {
                grdIngrediants.DataSource = Ingredientdata;

                grdIngrediants.Columns["Name"].DisplayIndex = 0;
                grdIngrediants.Columns["Value"].DisplayIndex = 1;
                grdIngrediants.Columns["Id"].Visible = false;

                grdIngrediants.Visible = true;
            }
            else
            {
                grdIngrediants.Visible = false;
            }

            if (cmbIngrediants.Items.Count <= 1)
            {
                cmbIngrediants.Text = "All Added !";
                btnIngredientAdd.Enabled = false;
            }
        }


        protected void FillCategories()
        {
            ProductCategoryCollection productCategoriesproducts = new ProductCategoryCollection()
                .Load();
            cmbCategory.DataSource = productCategoriesproducts;
        }

        private void FillIngredients()
        {
            addIngredientIds = new List<int>();

            if (SelectedRecordId > 0)
            {
                ProductIngredientCollection productIngredients = new ProductIngredientCollection()
                    .Where(ProductIngredient.Columns.ProductId, SelectedRecordId)
                    .Load();

                addIngredientIds = productIngredients.Select(x => x.IngredientId).ToList();
            }
            foreach (DataGridViewRow item in grdIngrediants.Rows)
            {
                int id = int.Parse(item.Cells["Id"].Value.ToString());
                addIngredientIds.Add(id);
            }
            if (remIngredientIds.Count > 0)
            {
                foreach (int _id in remIngredientIds)
                {
                    addIngredientIds.Remove(_id);
                }
            }
            addIngredientIds.Distinct();


            IngredientCollection ingredients = new IngredientCollection()
                .Where(Ingredient.Columns.Id, SubSonic.Comparison.NotIn, addIngredientIds)
                .Load();

            cmbIngrediants.DataSource = ingredients;
        }

        private void FillData()
        {
            Product product = new Product(SelectedRecordId);

            txtName.Text = product.Name;
            txtDes.Text = product.Description;
            txtPrice.Text = product.Price.ToString();

            cmbCategory.SelectedItem = product.ProductCategory.Name;

            ProductIngredientCollection productIngredients = new ProductIngredientCollection()
                .Where(ProductIngredient.Columns.ProductId, SelectedRecordId)
                .Load();

            foreach (var item in productIngredients)
            {
                DataRow dr = Ingredientdata.NewRow();

                dr["Id"] = item.Id;
                dr["Name"] = item.Ingredient.Name;
                dr["Value"] = item.Consumption;

                Ingredientdata.Rows.Add(dr);
            }

            grdIngrediants.DataSource = Ingredientdata;
            FillIngredients();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                Product product;
                ProductCategoryCollection categories = new ProductCategoryCollection().Where(ProductCategory.Columns.Name, cmbCategory.SelectedItem.ToString()).Load();

                if (SelectedRecordId > 0)
                {
                    product = new Product(SelectedRecordId);
                }
                else
                {
                    ProductCollection products = new ProductCollection()
                    .Where(Product.Columns.Name, txtName.Text)
                    .Load();
                    if (products.Count > 0)
                    {
                        MessageBox.Show("Product with Same Name already Exist !");
                        txtName.Text = "";
                        txtName.Focus();
                        product = null;
                    }
                    else
                    {
                        product = new Product();
                    }
                }
                if (product != null)
                {
                    product.Name = txtName.Text;
                    product.Description = txtDes.Text;
                    product.Price = Convert.ToInt32(txtPrice.Text);
                    product.CategoryId = categories[0].Id;

                    product.Save();

                    if (SelectedRecordId > 0)
                    {
                        ProductIngredientCollection oldIngredients = new ProductIngredientCollection().Where(ProductIngredient.Columns.ProductId, SelectedRecordId).Load();
                        foreach (int _id in oldIngredients.Select(x => x.Id))
                        {
                            ProductIngredient.Delete(_id);
                        }
                    }
                    foreach (DataGridViewRow item in grdIngrediants.Rows)
                    {
                        int id = int.Parse(item.Cells["Id"].Value.ToString());
                        int value = int.Parse(item.Cells["Value"].Value.ToString());

                        ProductIngredient productIngredient = new ProductIngredient();

                        productIngredient.ProductId = product.Id;
                        productIngredient.IngredientId = id;
                        productIngredient.Consumption = value;

                        productIngredient.Save();
                    }

                    this.Close();
                }
            }
        }

        private bool validated()
        {
            int _out;
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtDes.Text) || string.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("All Fields are Required !");
                return false;
            }
            if (!int.TryParse(txtPrice.Text, out _out))
            {
                MessageBox.Show("Price Must be in Number !");
                txtPrice.Text = "";
                txtPrice.Focus();
                return false;
            }
            else if (!(Convert.ToInt32(txtPrice.Text) > 0))
            {
                MessageBox.Show("Price Must be Greater then 0 !");
                txtPrice.Text = "";
                txtPrice.Focus();
                return false;
            }

            return true;
        }

        private void btnIngredientAdd_Click(object sender, EventArgs e)
        {
            AddIngredient();
            FillIngredients();
        }

        private void grdIngrediants_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataGridViewRow row = grdIngrediants.Rows[e.RowIndex];

                remIngredientIds.Add(int.Parse(row.Cells["Id"].Value.ToString()));

                grdIngrediants.Rows.Remove(row);


                btnIngredientAdd.Enabled = true;
                FillIngredients();
            }
        }
    }
}
