using DataAccess;
using kheelBrabriPrac.helpers;
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
    public partial class OrderDashboard : Form
    {
        DataTable data;
        bool isGridInitialized = false;
        private frmOrderProccess orderProccess;
        List<clSelectedProducts> selectedProducts = new List<clSelectedProducts>();

        public OrderDashboard()
        {
            InitializeComponent();
            FillCategories();
            FillProducts();
            FillDiscounts();
            txtPrice_TextChanged(null, null);
            InitializeData();
            updatePricing();
        }


        protected void FillCategories()
        {
            ProductCategoryCollection productCategoriesproducts = new ProductCategoryCollection()
                .Where(ProductCategory.Columns.Id, SubSonic.Comparison.In, new ProductCollection().Load().Select(x => x.CategoryId).ToList())
                .Load();
            cmbCategory.DataSource = productCategoriesproducts;

            if (productCategoriesproducts.Count <= 0)
            {
                cmbCategory.Text = "No Category Available Yet !";
            }
        }

        protected void FillDiscounts()
        {
            DiscountCollection discounts = new DiscountCollection()
                .Where(Discount.Columns.ToDate, SubSonic.Comparison.GreaterOrEquals, DateTime.Now)
                .Load();

            cmbDiscount.Items.Insert(0, "Select");

            for (int i = 0; i < discounts.Count; i++)
            {
                cmbDiscount.Items.Insert(i + 1, discounts[i].Name);
            }

            cmbDiscount.SelectedIndex = 0;


            if (discounts.Count <= 0)
            {
                cmbCategory.Text = "No Discounts Available Yet !";
            }
        }


        protected void FillProducts()
        {
            if (cmbCategory.Items.Count > 0)
            {
                int categoryId = new ProductCategoryCollection().Where(ProductCategory.Columns.Name, cmbCategory.SelectedItem.ToString()).Load().FirstOrDefault().Id;

                ProductCollection products = new ProductCollection()
                    .Where(Product.Columns.CategoryId, categoryId)
                    .Load();

                cmbProducts.DataSource = products;

                if (products.Count <= 0)
                {
                    cmbProducts.Text = "No Product Available Against this Category !";
                }
            }
            else
            {
                cmbProducts.Text = "Select Valid Category First !";
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProducts();
            updatePricing();
            if (Convert.ToInt32(txtDis.Text) < Convert.ToInt32(txtTotal.Text))
            {
                btnAddToCart.Enabled = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if (validated())
            {
                updatePricing();
            }
        }

        private bool validated()
        {
            int _out;
            if (string.IsNullOrEmpty(txtQty.Text) || string.IsNullOrEmpty(txtQty.Text))
            {
                if (!int.TryParse(txtQty.Text, out _out))
                {
                    MessageBox.Show("Cost Must be in Number !");
                    txtQty.Text = "";
                    txtQty.Focus();
                    return false;
                }
                else if (!(Convert.ToInt32(txtQty.Text) > 0))
                {
                    MessageBox.Show("Cost Must be Greater then 0 !");
                    txtQty.Text = "";
                    txtQty.Focus();
                    return false;
                }
                return false;
            }

            return true;
        }

        public void InitializeData()
        {
            if (!isGridInitialized)
            {
                data = new DataTable();

                DataColumn dataColumn_id = new DataColumn("Id", typeof(string));
                DataColumn dataColumn_name = new DataColumn("name", typeof(string));
                DataColumn dataColumn_price = new DataColumn("price", typeof(string));
                DataColumn dataColumn_qty = new DataColumn("qty", typeof(string));
                DataColumn dataColumn_dis = new DataColumn("discount", typeof(string));
                DataColumn dataColumn_cost = new DataColumn("cost", typeof(string));


                dataColumn_id.DefaultValue = 0;
                dataColumn_name.DefaultValue = "Name";
                dataColumn_price.DefaultValue = "Price";
                dataColumn_qty.DefaultValue = "Qty";
                dataColumn_dis.DefaultValue = "Discount";
                dataColumn_cost.DefaultValue = "Cost";

                data.Columns.Add(dataColumn_id);
                data.Columns.Add(dataColumn_name);
                data.Columns.Add(dataColumn_price);
                data.Columns.Add(dataColumn_dis);
                data.Columns.Add(dataColumn_qty);
                data.Columns.Add(dataColumn_cost);
            }
            isGridInitialized = true;

        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                updatePricing();
                Product product = new ProductCollection().Where(Product.Columns.Name, cmbProducts.SelectedItem.ToString()).Load().FirstOrDefault();

                int Remaining_Units = Convert.ToInt32(product.Unit) - selectedProducts.Where(x => x.product_id == product.Id).Sum(x => x.qty);
                if (Remaining_Units > Convert.ToInt32(txtQty.Text))
                {
                    InitializeData();

                    DataRow dr = data.NewRow();

                    dr["Id"] = product.Id;
                    dr["name"] = product.Name;
                    dr["price"] = product.Price;
                    dr["qty"] = txtQty.Text;
                    dr["discount"] = txtDis.Text;
                    dr["cost"] = txtNetTotal.Text;

                    data.Rows.Add(dr);

                    if (data.Rows.Count > 0)
                    {
                        grdCart.DataSource = data;
                        btnProcess.Enabled = true;
                    }
                    else
                    {
                        grdCart.DataSource = null;
                        btnProcess.Enabled = false;
                    }
                    updateAllPricing();


                    selectedProducts.Add(new clSelectedProducts
                    {
                        product_id = product.Id,
                        qty = Convert.ToInt32(txtQty.Text)
                    });

                }
                else
                {
                    MessageBox.Show("No Enough Quantity of Product is Remaining to Add !\nMake Inventory First for this Product");
                }


            }
        }

        protected void updatePricing()
        {
            Product product = new ProductCollection().Where(Product.Columns.Name, cmbProducts.SelectedItem.ToString()).Load().FirstOrDefault();

            if (cmbDiscount.SelectedIndex > 0)
            {
                Discount discount = new DiscountCollection().Where(Discount.Columns.Name, cmbDiscount.SelectedItem.ToString()).Load().FirstOrDefault();
                txtDis.Text = discount.ValueX.ToString();
            }
            else
            {
                txtDis.Text = (0).ToString();
            }

            txtTotal.Text = (product.Price * Convert.ToInt32(txtQty.Text)).ToString();

            txtNetTotal.Text = (Convert.ToInt32(txtTotal.Text) - Convert.ToInt32(txtDis.Text)).ToString();

            if (Convert.ToInt32(txtDis.Text) > Convert.ToInt32(txtTotal.Text))
            {
                btnAddToCart.Enabled = false;
                MessageBox.Show("Discount Can't Be Greater then Total Amount of Product !");
            }

        }

        protected void updateAllPricing()
        {
            int total = 0;
            int dis = 0;
            int netTotal = 0;

            //for (int i = 0; i < grdCart.Rows.Count; i++)
            //{
            //    total += Convert.ToInt32(grdCart.Rows[i].Cells["price"].Value);
            //    dis += Convert.ToInt32(grdCart.Rows[i].Cells["discount"].Value);
            //}
            foreach (DataGridViewRow row in grdCart.Rows)
            {
                total += (Convert.ToInt32(row.Cells["price"].Value) * Convert.ToInt32(row.Cells["qty"].Value));
                dis += Convert.ToInt32(row.Cells["discount"].Value);
            }

            txtAllTotal.Text = total.ToString();
            txtAllDic.Text = dis.ToString();
            txtAllNetTotal.Text = (total - dis).ToString();

        }


        private void grdCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = grdCart.Rows[e.RowIndex];

            grdCart.Rows.Remove(row);

            if (grdCart.Rows.Count <= 0)
            {
                btnProcess.Enabled = false;
            }
            updateAllPricing();
        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatePricing();
            if (Convert.ToInt32(txtDis.Text) < Convert.ToInt32(txtTotal.Text))
            {
                btnAddToCart.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtQty.Text = (1).ToString();
            data.Rows.Clear();
            grdCart.DataSource = data;
            FillDiscounts();
            FillCategories();
            FillProducts();
            txtPrice_TextChanged(null, null);
            InitializeData();
            updatePricing();
            updateAllPricing();
        }

        private void cmbDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatePricing();
            if (Convert.ToInt32(txtDis.Text) < Convert.ToInt32(txtTotal.Text))
            {
                btnAddToCart.Enabled = true;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            List<clOrder> allItems = new List<clOrder>();

            if (orderProccess != null)
            {
                orderProccess.Close();
            }
            foreach (DataGridViewRow row in grdCart.Rows)
            {
                clOrder item = new clOrder();

                item.product_id = Convert.ToInt32(row.Cells["Id"].Value);
                item.product_name = row.Cells["name"].Value.ToString();
                item.each_cost = Convert.ToInt32(row.Cells["price"].Value);
                item.qty = Convert.ToInt32(row.Cells["qty"].Value);
                item.discount = Convert.ToInt32(row.Cells["discount"].Value);
                item.cost = Convert.ToInt32(row.Cells["cost"].Value);

                allItems.Add(item);
            }
            orderProccess = new frmOrderProccess(allItems);
            orderProccess.Show();
        }
    }
}
