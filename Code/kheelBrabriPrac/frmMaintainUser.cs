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
    public partial class frmMaintainUser : Form
    {
        public int SelectedRecordId { get; set; }
        public frmMaintainUser(int selectedRecordId = -1)
        {
            InitializeComponent();

            if (selectedRecordId > 0)
            {
                lblLagend.Text = "Edit User Details";
                btnOperation.Text = "Update";

                SelectedRecordId = selectedRecordId;

                FillData();
            }
            else
            {
                lblLagend.Text = "Add New User";
                btnOperation.Text = "Save";
            }
        }

        private void FillData()
        {
            User user = new User(SelectedRecordId);

            txtName.Text = user.Username;
            txtEmail.Text = user.Email;
            txtPass.Text = user.Password;
            
        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                User user;

                if (SelectedRecordId > 0)
                {
                    user = new User(SelectedRecordId);
                }
                else
                {
                    user = new User();
                }

                user.Username = txtName.Text;
                user.Email = txtEmail.Text;
                user.Password = txtPass.Text;

                user.Save();

                this.Close();
            }
        }

        private bool validated()
        {
            int _out;
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Text))
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

        private void btnGen_Click(object sender, EventArgs e)
        {
            txtPass.Text = Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
