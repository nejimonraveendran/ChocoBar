using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChocoBar
{
    public partial class frmAddEditMyButton : Form
    {
        public frmAddEditMyButton()
        {
            InitializeComponent();
        }


        public Guid ButtonId { get; set; }
        public ButtonType ButtonType { get; set; }
        public bool IsEditMode { get; set; }
        public Keys PushButtonKeyCode { get; set; }
        public Keys SliderUpKeyCode { get; set; }
        public Keys SliderDownKeyCode { get; set; }
        public bool IsShift { get; set; }
        public bool IsCtrl { get; set; }
        public bool IsAlt { get; set; }
        public string ButtonText { get; set; }
        public string TooltipText { get; set; }
        public string Description { get; set; }
        public Color TextColor { get; set; }
        public Color BackgroundColor { get; set; }
        //public string IconPath { get; set; }
        public string IconBase64 { get; set; }


        private void frmAddEditMyButton_Load(object sender, EventArgs e)
        {
            setupForm();

        }


        private void setupForm()
        {
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.Text = "Add Button";
            txtText.MaxLength = 1;
            txtTooltip.MaxLength = 15;
            btnOk.Enabled = false;
            radPush.Checked = true;
            this.CancelButton = btnCancel;
            this.AcceptButton = btnOk;
            txtButtonAndSliderUpShortcut.ReadOnly = true;
            txtSliderUpShortcut.ReadOnly = true;
            picIcon.SizeMode = PictureBoxSizeMode.CenterImage;


            picBackColor.BackColor = Color.FromArgb(40, 40, 40);
            picForeColor.BackColor = Color.White;

            if (this.IsEditMode)
            {
                this.Text = "Edit Button";

                radPush.Checked = this.ButtonType == ButtonType.Push;
                radSlider.Checked = this.ButtonType == ButtonType.Slider;
                picBackColor.BackColor = this.BackgroundColor;
                picForeColor.BackColor = this.TextColor;
                txtText.Text = this.ButtonText;
                txtTooltip.Text = this.Description;

                //if(!string.IsNullOrEmpty(this.IconPath) && File.Exists(this.IconPath))
                //{
                //    picIcon.Image = ImageOps.Base64ToImage(this.IconBase64); //Image.FromFile(this.IconPath);
                //}

                if (!string.IsNullOrEmpty(this.IconBase64))
                {
                    picIcon.Image = ImageOps.Base64ToImage(this.IconBase64);
                }

                if (radPush.Checked)
                {
                    setShortCutText(txtButtonAndSliderUpShortcut, this.IsAlt, this.IsShift, this.IsCtrl, this.PushButtonKeyCode);
                }
                else if (radSlider.Checked)
                {
                    setShortCutText(txtButtonAndSliderUpShortcut, this.IsAlt, this.IsShift, this.IsCtrl, this.SliderDownKeyCode);
                    setShortCutText(txtSliderUpShortcut, this.IsAlt, this.IsShift, this.IsCtrl, this.SliderUpKeyCode);

                }

            }
            else
            {
                this.ButtonId = Guid.NewGuid();
            }
        }

        private void radPush_CheckedChanged(object sender, EventArgs e)
        {
            if (radPush.Checked)
            {
                lblShortcut.Text = "Button shortcut";
                txtSliderUpShortcut.Enabled = false;
                txtSliderUpShortcut.BackColor = Color.LightGray;
                txtSliderUpShortcut.Text = string.Empty;
                txtButtonAndSliderUpShortcut.Enabled = true;
                txtButtonAndSliderUpShortcut.BackColor = Color.White;
            }
        }

        private void radSlider_CheckedChanged(object sender, EventArgs e)
        {
            if (radSlider.Checked)
            {
                lblShortcut.Text = "Slider up shortcut";
                txtButtonAndSliderUpShortcut.Enabled = true;
                txtButtonAndSliderUpShortcut.BackColor = Color.White;
                txtSliderUpShortcut.Enabled = true;
                txtSliderUpShortcut.BackColor = Color.White;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (radPush.Checked)
            {
                this.ButtonType = ButtonType.Push;
                this.SliderUpKeyCode = Keys.None;
                this.SliderDownKeyCode = Keys.None;
                this.TooltipText = $"{txtTooltip.Text.Trim()}({this.txtButtonAndSliderUpShortcut.Text.Trim()})";

            }
            else if (radSlider.Checked)
            {
                this.ButtonType = ButtonType.Slider;
                this.PushButtonKeyCode = Keys.None;
                this.TooltipText = $"{txtTooltip.Text.Trim()}({this.txtButtonAndSliderUpShortcut.Text.Trim()}, {this.txtSliderUpShortcut.Text.Trim()})";
            }

            this.ButtonText = txtText.Text.Trim();
            this.Description = txtTooltip.Text.Trim();
            this.TextColor = picForeColor.BackColor;
            this.BackgroundColor = picBackColor.BackColor;
            this.IconBase64 = picIcon.Image != null ? ImageOps.ImageToBase64(picIcon.Image) : null;

            //if (!saveIcon())
            //{
            //    return;
            //}

            this.DialogResult = DialogResult.OK;
            this.Close();

        }


        //private bool saveIcon()
        //{
        //    try
        //    {
        //        if (picIcon.Image != null && picIcon.Tag != null)
        //        {
        //            string iconPath = Path.Combine(Environment.CurrentDirectory, "Icons");

        //            if (!Directory.Exists(iconPath))
        //            {
        //                Directory.CreateDirectory(iconPath);
        //            }

        //            string iconFileName = Path.Combine(iconPath, $"{this.ButtonId}.png");
        //            File.Copy(picIcon.Tag.ToString(), iconFileName, true);

        //            this.IconPath = iconFileName;

        //            return true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //log
        //        MessageBox.Show("Unexpected error while saving the icon", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }

        //    return true;
        //}

        private void txtText_TextChanged(object sender, EventArgs e)
        {
            enableDisableOkButton();
        }

        private void txtTooltip_TextChanged(object sender, EventArgs e)
        {
            enableDisableOkButton();
        }

        private void txtButtonShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            this.IsAlt = false;
            this.IsCtrl = false;
            this.IsShift = false;
            this.PushButtonKeyCode = Keys.None;
            this.SliderDownKeyCode = Keys.None;
            txtButtonAndSliderUpShortcut.Text = string.Empty;

            if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.Menu)
                return;

            this.IsAlt = e.Alt;
            this.IsCtrl = e.Control;
            this.IsShift = e.Shift;

            this.PushButtonKeyCode = e.KeyCode;
            this.SliderDownKeyCode = e.KeyCode;

            if (e.KeyData == Keys.Up)
            {
                this.PushButtonKeyCode = Keys.None;
                this.SliderDownKeyCode = Keys.Up;
            }

            setShortCutText(txtButtonAndSliderUpShortcut, e.Alt, e.Shift, e.Control, e.KeyCode);

        }


        private void txtSliderUpShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            this.IsAlt = false;
            this.IsCtrl = false;
            this.IsShift = false;
            this.SliderUpKeyCode = Keys.None;
            txtSliderUpShortcut.Text = string.Empty;

            if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.Menu)
                return;

            this.IsAlt = e.Alt;
            this.IsCtrl = e.Control;
            this.IsShift = e.Shift;
            this.SliderUpKeyCode = e.KeyCode;

            setShortCutText(txtSliderUpShortcut, e.Alt, e.Shift, e.Control, e.KeyCode);
        }


        void setShortCutText(TextBox textBox, bool isAlt, bool isShift, bool isCtrl, Keys keyCode)
        {
            string key = string.Empty;

            if (isAlt)
            {
                key = "Alt+";
            }

            if (isShift)
            {
                key += "Shift+";
            }

            if (isCtrl)
            {
                key += "Ctrl+";
            }

            key = keyCode == Keys.None ? string.Empty : key + keyCode;

            textBox.Text = key;
        }


        void enableDisableOkButton()
        {
            btnOk.Enabled = false;

            if (radPush.Checked)
            {
                btnOk.Enabled = !string.IsNullOrEmpty(txtTooltip.Text) &&
                    !string.IsNullOrEmpty(txtButtonAndSliderUpShortcut.Text) &&
                    (!string.IsNullOrEmpty(txtText.Text) || picIcon.Image != null);
            }
            else if (radSlider.Checked)
            {
                btnOk.Enabled = !string.IsNullOrEmpty(txtTooltip.Text) &&
                    !string.IsNullOrEmpty(txtSliderUpShortcut.Text) &&
                    !string.IsNullOrEmpty(txtSliderUpShortcut.Text) &&
                    (!string.IsNullOrEmpty(txtText.Text) || picIcon.Image != null);

            }
        }

        private void txtButtonShortcut_TextChanged(object sender, EventArgs e)
        {
            enableDisableOkButton();
        }

        private void txtSliderUpShortcut_TextChanged(object sender, EventArgs e)
        {
            enableDisableOkButton();
        }


        private void picForeColor_Click(object sender, EventArgs e)
        {
            if (clrDlg.ShowDialog(this) == DialogResult.OK)
            {
                picForeColor.BackColor = clrDlg.Color;
            }

        }

        private void picBackColor_Click(object sender, EventArgs e)
        {
            if (clrDlg.ShowDialog(this) == DialogResult.OK)
            {
                picBackColor.BackColor = clrDlg.Color;
            }

        }

        private void picIcon_Click(object sender, EventArgs e)
        {
            dlgOpenFile.Filter = "PNG files (*.png)|*.png";
            dlgOpenFile.Title = "Select an icon (PNG)";
            dlgOpenFile.Multiselect = false;
            dlgOpenFile.CheckFileExists = true;
            dlgOpenFile.CheckPathExists = true;
            dlgOpenFile.RestoreDirectory = true;

            if (dlgOpenFile.ShowDialog(this) != DialogResult.OK)
                return;

            picIcon.Image = ImageOps.ImageFileToImage(dlgOpenFile.FileName);  //Image.FromFile(dlgOpenFile.FileName);
            //picIcon.Tag = dlgOpenFile.FileName;

            enableDisableOkButton();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtButtonAndSliderUpShortcut.Clear();
            txtSliderUpShortcut.Clear();
            txtText.Clear();
            txtTooltip.Clear();
            picForeColor.BackColor = Color.White;
            picBackColor.BackColor = Color.FromArgb(40, 40, 40);
            picIcon.Image = null;
            //picIcon.Tag = null;
            radPush.Checked = true;

        }

        private void txtButtonAndSliderUpShortcut_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
            }
        }

        private void txtSliderUpShortcut_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
            }

        }
    }
}
