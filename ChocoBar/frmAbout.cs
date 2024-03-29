using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChocoBar
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close ();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openLink(((LinkLabel)sender).Text);
        }

        private void openLink(string url)
        {
            Process.Start(url);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openLink(((LinkLabel)sender).Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openLink(((LinkLabel)sender).Text);
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            this.lblProductName.Text = $"{Application.ProductName}";
            this.lblProductVersion.Text = $"{Application.ProductVersion}";

        }
    }
}
