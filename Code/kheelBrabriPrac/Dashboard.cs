using DataAccess;
using kheelBrabriPrac.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kheelBrabriPrac
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            User user = UserSession.CurrentUser;
            lblUserName.Text = user.Username;
            button4_Click(null, null);
        }
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            UserSession.CurrentUser = null;
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserSession.CurrentUser = null;
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("yyyy-MMM-dd h:mm tt");
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            
            Products products = new Products();
            products.TopLevel = false;
            pnlContaintHolder.Controls.Clear();
            pnlContaintHolder.Controls.Add(products);
            products.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            products.Dock = DockStyle.Fill;
            products.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product_Category productCategory = new Product_Category();
            productCategory.TopLevel = false;
            pnlContaintHolder.Controls.Clear();
            pnlContaintHolder.Controls.Add(productCategory);
            productCategory.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            productCategory.Dock = DockStyle.Fill;
            productCategory.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmIngredient productCategory = new frmIngredient();
            productCategory.TopLevel = false;
            pnlContaintHolder.Controls.Clear();
            pnlContaintHolder.Controls.Add(productCategory);
            productCategory.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            productCategory.Dock = DockStyle.Fill;
            productCategory.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.TopLevel = false;
            pnlContaintHolder.Controls.Clear();
            pnlContaintHolder.Controls.Add(inventory);
            inventory.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            inventory.Dock = DockStyle.Fill;
            inventory.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OrderDashboard inventory = new OrderDashboard();
            inventory.TopLevel = false;
            pnlContaintHolder.Controls.Clear();
            pnlContaintHolder.Controls.Add(inventory);
            inventory.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            inventory.Dock = DockStyle.Fill;
            inventory.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmDiscount discount = new frmDiscount();
            discount.TopLevel = false;
            pnlContaintHolder.Controls.Clear();
            pnlContaintHolder.Controls.Add(discount);
            discount.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            discount.Dock = DockStyle.Fill;
            discount.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmOrdersList ordersList = new frmOrdersList();
            ordersList.TopLevel = false;
            pnlContaintHolder.Controls.Clear();
            pnlContaintHolder.Controls.Add(ordersList);
            ordersList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ordersList.Dock = DockStyle.Fill;
            ordersList.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmUsersList UsersList = new frmUsersList();
            UsersList.TopLevel = false;
            pnlContaintHolder.Controls.Clear();
            pnlContaintHolder.Controls.Add(UsersList);
            UsersList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            UsersList.Dock = DockStyle.Fill;
            UsersList.Show();
        }
    }
}
