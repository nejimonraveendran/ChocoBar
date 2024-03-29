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
    public partial class frmWindowShot : Form
    {
        public frmWindowShot()
        {
            InitializeComponent();
        }

        public string VideosFolder { get; set; }
        public string ProcessName { get; private set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmWindowShot_Load(object sender, EventArgs e)
        {

            lnkVideosFolder.Text = VideosFolder;
            btnOk.Enabled = false;  
            cmbProcesses.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbProcesses.Items.Clear();

            var processes = Process.GetProcesses()
                .Where(p => !string.IsNullOrEmpty(p.MainWindowTitle) && Win32.IsWindowVisible(p.MainWindowHandle))
                .ToList();
  
            var comboBoxItems = new List<ComboBoxItem>();   

            processes.ForEach(p =>
            {
                comboBoxItems.Add(new ComboBoxItem 
                {
                    Text = $"{p.ProcessName} - {p.MainWindowTitle}",
                    Value = p.ProcessName
                });
            });


            cmbProcesses.DataSource = comboBoxItems.OrderBy(p => p.Value).ToList();
            cmbProcesses.DisplayMember = "Text";
            cmbProcesses.ValueMember = "Value";

            if (cmbProcesses.Items.Count > 0)
            {
                cmbProcesses.SelectedIndex = 0;
            }
        }

        private void cmbProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
           btnOk.Enabled = cmbProcesses.SelectedIndex >= 0 && !string.IsNullOrEmpty(cmbProcesses.Text);        
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.ProcessName = (string) cmbProcesses.SelectedValue;
            this.Close();
        }

        private void lnkVideosFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", lnkVideosFolder.Text);
        }
    }


    internal class ComboBoxItem
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
