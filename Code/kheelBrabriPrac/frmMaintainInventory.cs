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
    public partial class frmMaintainInventory : Form
    {
        public frmMaintainInventory()
        {
            InitializeComponent();
            fillProducts();
        }

        private void fillProducts()
        {
            ProductCollection products = new ProductCollection()
                .Load();

            cmbProduct.DataSource = products.Select(x => x.Name).ToList();
        }
        
        private void btnOperation_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                int ProductId = new ProductCollection().Where(Product.Columns.Name, cmbProduct.SelectedItem.ToString()).Load().FirstOrDefault().Id;

                InventoryLog inventoryLog = new InventoryLog();

                inventoryLog.ProductId = ProductId;
                inventoryLog.Quantity = Convert.ToInt32(txtQty.Text);
                inventoryLog.Cost = Convert.ToInt32(txtCost.Text);

                inventoryLog.Save();

                Product product = new Product(ProductId);
                product.Unit = Convert.ToInt32(product.Unit) + inventoryLog.Quantity;
                product.Save();

                this.Close();
            }
        }

        private bool validated()
        {
            int _out;
            if (string.IsNullOrEmpty(txtCost.Text) || string.IsNullOrEmpty(txtQty.Text))
            {
                if (!int.TryParse(txtCost.Text, out _out))
                {
                    MessageBox.Show("Cost Must be in Number !");
                    txtCost.Text = "";
                    txtCost.Focus();
                    return false;
                }
                else if (!(Convert.ToInt32(txtCost.Text) > 0))
                {
                    MessageBox.Show("Cost Must be Greater then 0 !");
                    txtCost.Text = "";
                    txtCost.Focus();
                    return false;
                }

                else if (!int.TryParse(txtQty.Text, out _out))
                {
                    MessageBox.Show("Quantity Must be in Number !");
                    txtQty.Text = "";
                    txtQty.Focus();
                    return false;
                }
                else if (!(Convert.ToInt32(txtQty.Text) > 0))
                {
                    MessageBox.Show("Quantity Must be Greater then 0 !");
                    txtQty.Text = "";
                    txtQty.Focus();
                    return false;
                }
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
