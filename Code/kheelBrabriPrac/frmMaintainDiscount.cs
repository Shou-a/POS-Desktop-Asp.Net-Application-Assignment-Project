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
    public partial class frmMaintainDiscount : Form
    {
        public int SelectedRecordId { get; set; }
        public frmMaintainDiscount(int selectedRecordId = -1)
        {
            InitializeComponent();

            if (selectedRecordId > 0)
            {
                lblLagend.Text = "Edit Discount";
                btnOperation.Text = "Update";

                SelectedRecordId = selectedRecordId;

                FillData();
            }
            else
            {
                lblLagend.Text = "Add Discount";
                btnOperation.Text = "Save";

                dtpTo.Value = DateTime.Now.AddMonths(1);
            }
        }

        private void FillData()
        {
            Discount discount = new Discount(SelectedRecordId);

            txtName.Text = discount.Name;
            txtDes.Text = discount.Description;
            txtValue.Text = discount.ValueX.ToString();

            dtpFrom.Value = discount.FromDate.Value;
            dtpTo.Value = discount.ToDate.Value;
        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                Discount discount;

                if (SelectedRecordId > 0)
                {
                    discount = new Discount(SelectedRecordId);
                }
                else
                {
                    discount = new Discount();
                }

                discount.Name = txtName.Text;
                discount.Description = txtDes.Text;
                discount.ValueX = Convert.ToInt32(txtValue.Text);
                discount.FromDate = dtpFrom.Value;
                discount.ToDate = dtpTo.Value;

                discount.Save();

                this.Close();
            }
        }

        private bool validated()
        {
            int _out;
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtDes.Text) || string.IsNullOrEmpty(txtValue.Text))
            {
                MessageBox.Show("All Fields are Required !");
                return false;
            }
            else if (!int.TryParse(txtValue.Text, out _out))
            {
                MessageBox.Show("Value Must be in Number !");
                txtValue.Text = "";
                txtValue.Focus();
                return false;
            }
            else if (dtpTo.Value < dtpFrom.Value)
            {
                MessageBox.Show("To Date Can't Be Less then From Date !");
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
