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
    public partial class frmUsersList : Form
    {
        private frmMaintainUser maintain;
        bool isGridInitialized = false;
        DataTable data;

        public frmUsersList()
        {
            InitializeComponent();
            FillData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            maintain = new frmMaintainUser();
            maintain.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillData();
        }
        public void InitializeGrd()
        {
            if (!isGridInitialized)
            {
                data = new DataTable();

                DataColumn dataColumn_id = new DataColumn("Id", typeof(string));
                DataColumn dataColumn_name = new DataColumn("name", typeof(string));
                DataColumn dataColumn_email = new DataColumn("email", typeof(string));


                dataColumn_id.DefaultValue = 0;
                dataColumn_name.DefaultValue = "Name";
                dataColumn_email.DefaultValue = "Email";

                data.Columns.Add(dataColumn_id);
                data.Columns.Add(dataColumn_name);
                data.Columns.Add(dataColumn_email);


            }
            isGridInitialized = true;

        }
        protected void FillData()
        {
            InitializeGrd();


            data.Rows.Clear();

            UserCollection users = new UserCollection().Load();

            foreach (var user in users)
            {
                DataRow dr = data.NewRow();

                dr["Id"] = user.Id;
                dr["name"] = user.Username;
                dr["email"] = user.Email;

                data.Rows.Add(dr);
            }



            if (data.Rows.Count > 0)
            {
                grdItems.DataSource = data;

                grdItems.Columns["name"].DisplayIndex = 0;
                grdItems.Columns["email"].DisplayIndex = 1;
                grdItems.Columns["Id"].Visible = false;

                grdItems.Visible = true;
            }
            else
            {
                grdItems.Visible = false;
            }

        }


        private void grdItems_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int id = int.Parse(grdItems.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                if (maintain != null)
                {
                    maintain.Close();
                }
                maintain = new frmMaintainUser(id);
                maintain.Show();

            }
            else if (e.ColumnIndex == 1)
            {
                int id = int.Parse(grdItems.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                try
                {
                    User.Delete(id);
                }
                catch (Exception)
                {
                    MessageBox.Show("Can't Delete !\nUser haven't enough Rights to Delete.");
                }
            }

            FillData();
        }
    }
}
