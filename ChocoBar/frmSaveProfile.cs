using System;
using System.Windows.Forms;

namespace ChocoBar
{
    public partial class frmSaveProfile : Form
    {
        public frmSaveProfile()
        {
            InitializeComponent();
        }

        public string ProfileName { get; set; }

        private void txtText_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = !string.IsNullOrEmpty(txtProfileName.Text.Trim());
        }

        private void frmSaveProfile_Load(object sender, EventArgs e)
        {
            btnOk.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.ProfileName = txtProfileName.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
