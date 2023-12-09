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
    public partial class maintainCategory : Form
    {
        public int SelectedRecordId { get; set; }
        public maintainCategory(int selectedRecordId = -1)
        {
            InitializeComponent();

            if (selectedRecordId > 0)
            {
                lblLagend.Text = "Edit Category";
                btnOperation.Text = "Update";

                SelectedRecordId = selectedRecordId;

                FillData();
            }
            else
            {
                lblLagend.Text = "Add Category";
                btnOperation.Text = "Save";
            }
        }
        private void FillData()
        {
            ProductCategory product = new ProductCategory(SelectedRecordId);

            txtName.Text = product.Name;
            txtDes.Text = product.Description;

        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                ProductCategory category;

                if (SelectedRecordId > 0)
                {
                    category = new ProductCategory(SelectedRecordId);
                }
                else
                {
                    ProductCategoryCollection categories = new ProductCategoryCollection()
                        .Where(ProductCategory.Columns.Name, txtName.Text)
                        .Load();

                    if (categories.Count > 0)
                    {
                        MessageBox.Show("Product with Same Name already Exist !");
                        txtName.Text = "";
                        txtName.Focus();
                        category = null;
                    }
                    else
                    {
                        category = new ProductCategory();
                    }
                }
                if (category != null)
                {
                    category.Name = txtName.Text;
                    category.Description = txtDes.Text;

                    category.Save();

                    this.Close();
                }
            }
        }

        private bool validated()
        {
            int _out;
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtDes.Text))
            {
                MessageBox.Show("All Fields are Required !");
                return false;
            }

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
