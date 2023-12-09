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
    public partial class frmMaintainIngredient : Form
    {
        public int SelectedRecordId { get; set; }
        public frmMaintainIngredient(int selectedRecordId = -1)
        {
            InitializeComponent();

            if (selectedRecordId > 0)
            {
                lblLagend.Text = "Edit Ingrediant";
                btnOperation.Text = "Update";

                SelectedRecordId = selectedRecordId;

                FillData();
            }
            else
            {
                lblLagend.Text = "Add Ingrediant";
                btnOperation.Text = "Save";
            }
        }
        private void FillData()
        {
            Ingredient ingredient = new Ingredient(SelectedRecordId);

            txtName.Text = ingredient.Name;
            txtDes.Text = ingredient.Description;
        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                Ingredient ingredient;

                if (SelectedRecordId > 0)
                {
                    ingredient = new Ingredient(SelectedRecordId);
                }
                else
                {
                    ingredient = new Ingredient();
                }

                ingredient.Name = txtName.Text;
                ingredient.Description = txtDes.Text;
                ingredient.Unit = 0;

                ingredient.Save();

                this.Close();
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
