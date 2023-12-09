using DataAccess;
using kheelBrabriPrac.common;
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
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserCollection users = new UserCollection()
                .Where(User.Columns.Email, txtUName.Text)
                .Where(User.Columns.Password, txtPass.Text)
                .Load();
            if(users.Count > 0)
            {
                UserSession.CurrentUser = users.FirstOrDefault();
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Wrong User Name or Password !");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
