using DataAccess;
using kheelBrabriPrac.helpers;
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
    public partial class splashScreen : Form
    {
        threadHelper threadHelper;
        Task task;
        public splashScreen()
        {
            InitializeComponent();
        }
        
        

        private void UpdateProgressBar(int value)
        {
            if (progBar.InvokeRequired)
            {
                progBar.Invoke((MethodInvoker)(() => { progBar.Value = value; }));
            }
            else
            {
                progBar.Value = value;
            }
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void onLoad()
        {
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progBar.Increment(10);
            if (progBar.Value >= 100)
            {
                timer1.Stop();
                onLoad();
            }
        }
    }
}
