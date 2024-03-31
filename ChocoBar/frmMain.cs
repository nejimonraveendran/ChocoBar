using ChocoBar.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace ChocoBar
{

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        enum BarThickness
        {
            Small = 50,
            Medium = 70,
            Large = 90
        }


        Profile _profile = new Profile
        {
            ProfileName = null,
            BarEdge = Win32.AppBarEdge.ABE_RIGHT,
            BarThickness = (int)BarThickness.Small,
            SliderResolution = 5,
            ButtonConfigs = new List<ButtonConfig>(),
            ButtonResizeZoneWidth = 10

        };


        AppBarInfo _appBarInfo;
        ContextMenuStrip _mnuMainContext;
        ContextMenuStrip _mnuButtonContext;
        ToolStripMenuItem _mnuLoadProfiles;
        ToolStripMenuItem _mnuDeleteProfiles;
        Button _btnMenu;
        Button _btnAddNew;
        private readonly ButtonResizeInfo _btnResizeInfo = new ButtonResizeInfo();
        bool _configChanged = false;
        string _defaultProfileName = "DefaultProfile";
        string _defaultProfileDir;
        string _profilesDir;
        int _lastSliderX = 0;
        int _lastSliderY = 0;
        Win32.AppBarEdge _curBarEdge;
        private bool _timeLapeStarted = false;
        private ToolStripItem _mnuTimeLapase;
        private string _videosFolder;


        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName;

            try
            {
                _defaultProfileDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);
                _profilesDir = Path.Combine(_defaultProfileDir, "Profiles");
                _videosFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), Application.ProductName);

                if (!Directory.Exists(_defaultProfileDir))
                {
                    Directory.CreateDirectory(_defaultProfileDir);
                }

                if (!Directory.Exists(_profilesDir))
                {
                    Directory.CreateDirectory(_profilesDir);
                }

                if (!Directory.Exists(_videosFolder))
                {
                    Directory.CreateDirectory(_videosFolder);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create required directories.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //set base window styles
            Win32.SetWindowLongW(this.Handle, Win32.GWL.GWL_STYLE, Win32.WindowStyles.WS_POPUP | Win32.WindowStyles.WS_BORDER);
            Win32.SetWindowLongW(this.Handle, Win32.GWL.GWL_EXSTYLE, Win32.WindowStyles.WS_EX_NOACTIVATE | Win32.WindowStyles.WS_EX_TOOLWINDOW);
            Win32.SetWindowPos(this.Handle, Win32.HwndInsertAfter.HWND_TOPMOST, 0, 0, 0, 0, Win32.SetWindowPosFlags.SWP_NOMOVE | Win32.SetWindowPosFlags.SWP_NOSIZE);


            loadProfile(_defaultProfileDir, _defaultProfileName);
            setupNewAppBar();

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_configChanged)
            {
                if (MessageBox.Show("Button config has changed. \nDo you want to save the current profile as default?", "Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    saveProfileAsDefault();
                }
            }

            WindowShot.StopRecording();
            removeAppBar();
        }


        private void frmMain_DisplayChanged(int bitsPerPixel, int horizontalResolution, int verticalResolution)
        {
            //removeAppBar();
            //setupNewAppBar();
        }

        void setupNewAppBar()
        {
            _appBarInfo = null;
            _appBarInfo = AppBar.RegisterAppBar(this.Handle, _profile.BarThickness, _profile.BarEdge);

            frmMain_AppBarPositionChanged(_appBarInfo);
        }

        private void removeAppBar()
        {
            clearControls();
            AppBar.UnRegisterAppBar(this);
        }


        private void frmMain_AppBarPositionChanged(AppBarInfo appBarInfo)
        {
            positionToolBar(appBarInfo);
            clearControls();
            setupMainContextMenu();
            setupButtonContextMenu();
            setupMenuButton();
            buildMyButtonsFromCurrentProfile();
            setupAddNewButton();
            reArrangeButtons();
        }


        private void positionToolBar(AppBarInfo appBarInfo)
        {
            this._curBarEdge = appBarInfo.AppBarData.uEdge;
            this.Left = appBarInfo.AppBarData.rc.Left;
            this.Top = appBarInfo.AppBarData.rc.Top;


            this.Width = appBarInfo.AppBarData.uEdge == Win32.AppBarEdge.ABE_LEFT ||
                appBarInfo.AppBarData.uEdge == Win32.AppBarEdge.ABE_RIGHT ? _profile.BarThickness : appBarInfo.AppBarData.rc.Right;

            this.Height = appBarInfo.AppBarData.uEdge == Win32.AppBarEdge.ABE_LEFT ||
                appBarInfo.AppBarData.uEdge == Win32.AppBarEdge.ABE_RIGHT ? Screen.PrimaryScreen.WorkingArea.Height : appBarInfo.AppBarData.rc.Bottom;

            this.TopMost = true;

        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            addNewMyButton();
            setupAddNewButton();
        }


        private void MyButton_Click(object sender, EventArgs e)
        {
            var btn = (MyButton)sender;
            Input.SimulateKeyDown((int)btn.ButtonKeyCode, btn.IsShift, btn.IsCtrl, btn.IsAlt);
            Input.SimulateKeyUp((int)btn.ButtonKeyCode, btn.IsShift, btn.IsCtrl, btn.IsAlt);
        }

        private void MyButton_MouseEnter(object sender, EventArgs e)
        {
            showToolTip((MyButton)sender);
        }

        private void MyButton_MouseHover(object sender, EventArgs e)
        {
            showToolTip((MyButton)sender);
        }


        private void MyButton_PointerEnter(object sender, PointerEventArgs e)
        {
            showToolTip((MyButton)sender);
        }


        private void showToolTip(MyButton btn)
        {
            toolTip.Show(btn.TooltipText, btn, 1000);
        }

        private void MyButton_PointerDown(object sender, PointerEventArgs e)
        {
            var btn = (MyButton)sender;
            btn.BackgroundImage = null;

            _lastSliderX = e.X;
            _lastSliderY = e.Y;
        }

        private void MyButton_PointerUp(object sender, PointerEventArgs e)
        {
            var btn = (MyButton)sender;
            btn.BackgroundImage = null;
        }


        private void MyButton_PointerLeave(object sender, PointerEventArgs e)
        {
            var btn = (MyButton)sender;
            btn.BackgroundImage = null;

        }

        private void MyButton_PointerUpdate(object sender, PointerEventArgs e)
        {
            var btn = (MyButton)sender;

            if (btn.ButtonType != ButtonType.Slider || !e.IsInContact)
            {
                return;
            }

            if (_curBarEdge == Win32.AppBarEdge.ABE_LEFT || _curBarEdge == Win32.AppBarEdge.ABE_RIGHT)
            {
                if (Math.Abs(e.Y - _lastSliderY) > _profile.SliderResolution)
                {
                    if (e.Y > _lastSliderY) //pointer moving down
                    {
                        Input.SimulateKeyDown((int)btn.SliderDownKeyCode, btn.IsShift, btn.IsCtrl, btn.IsAlt);
                        Input.SimulateKeyUp((int)btn.SliderDownKeyCode, btn.IsShift, btn.IsCtrl, btn.IsAlt);

                    }
                    else //pointer moving up
                    {
                        Input.SimulateKeyDown((int)btn.SliderUpKeyCode, btn.IsShift, btn.IsCtrl, btn.IsAlt);
                        Input.SimulateKeyUp((int)btn.SliderUpKeyCode, btn.IsShift, btn.IsCtrl, btn.IsAlt);
                    }

                    _lastSliderY = e.Y;
                }
            }
            else if (_curBarEdge == Win32.AppBarEdge.ABE_TOP)
            {
                if (Math.Abs(e.X - _lastSliderX) > _profile.SliderResolution)
                {
                    if (e.X > _lastSliderX)
                    {
                        //sendKeyToRebelle(btn.SliderUpKeyCodes[0]);
                    }
                    else
                    {
                        //sendKeyToRebelle(btn.SliderDownKeyCodes[0]);
                    }

                    _lastSliderX = e.X;
                }
            }

        }


        void addNewMyButton()
        {
            if (!canAddNewButton())
            {
                MessageBox.Show("Not enough space to add new btn.\nPlease remove/resize other btn(s).", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            using (var frm = new frmAddEditMyButton())
            {
                frm.IsEditMode = false;

                if (frm.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                var btn = new MyButton();

                btn.Id = frm.ButtonId;
                btn.ButtonType = frm.ButtonType;
                btn.ButtonText = frm.ButtonText;
                btn.TooltipText = frm.TooltipText;
                btn.Description = frm.Description;
                btn.ForeColor = frm.TextColor;
                btn.BackColor = frm.BackgroundColor;
                btn.ButtonKeyCode = frm.PushButtonKeyCode;
                btn.SliderUpKeyCode = frm.SliderUpKeyCode;
                btn.SliderDownKeyCode = frm.SliderDownKeyCode;
                btn.IsShift = frm.IsShift;
                btn.IsCtrl = frm.IsCtrl;
                btn.IsAlt = frm.IsAlt;
                btn.FlatStyle = FlatStyle.Popup;
                btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                btn.ImageAlign = ContentAlignment.MiddleCenter;
                btn.IconBase64 = frm.IconBase64;
                btn.Left = _btnAddNew.Left;
                btn.Top = _btnAddNew.Top;
                btn.Width = _btnAddNew.Width;
                btn.Height = _btnAddNew.Height;
                btn.ContextMenuStrip = _mnuButtonContext;


                if (!string.IsNullOrEmpty(frm.IconBase64))
                {
                    btn.Image = ImageOps.Base64ToImage(frm.IconBase64);
                }
                else if (!string.IsNullOrEmpty(frm.ButtonText))
                {
                    btn.Text = frm.ButtonText;
                }
                else
                {
                    btn.Image = Resources.Sad_32;
                }


                btn.Click += MyButton_Click;
                btn.PointerEnter += MyButton_PointerEnter;
                btn.PointerDown += MyButton_PointerDown;
                btn.PointerUpdate += MyButton_PointerUpdate;
                btn.PointerLeave += MyButton_PointerLeave;
                btn.MouseEnter += MyButton_MouseEnter;
                btn.MouseHover += MyButton_MouseHover;
                btn.PointerUp += MyButton_PointerUp;
                btn.MouseDown += MyButton_MouseDown;
                btn.MouseUp += MyButton_MouseUp;
                btn.MouseMove += MyButton_MouseMove;
                btn.MouseLeave += MyButton_MouseLeave;  
                btn.DragOver += MyButton_DragOver;
                btn.DragDrop += MyButton_DragDrop;


                this.Controls.Add(btn);

                _configChanged = true;


            }

        }


        private bool canAddNewButton()
        {
            if (_curBarEdge == Win32.AppBarEdge.ABE_LEFT || _curBarEdge == Win32.AppBarEdge.ABE_RIGHT)
            {
                return _btnAddNew.Bottom < this.ClientRectangle.Bottom - this.Width;
            }
            else if (_curBarEdge == Win32.AppBarEdge.ABE_TOP)
            {
                //return _btnAddNew.Right < this.ClientRectangle.Right;
            }

            return false;
        }


        private void MyButton_MouseLeave(object sender, EventArgs e)
        {
            _btnResizeInfo.IsResizing = false;
            _btnResizeInfo.CanResize = false;
        }

        private void MyButton_MouseUp(object sender, MouseEventArgs e)
        {
            _btnResizeInfo.IsResizing = false;
            _btnResizeInfo.PrevPoint = e.Location;

        }


        MyButton _draggedButton = null; 

        private void MyButton_MouseDown(object sender, MouseEventArgs e)
        {
            //if(e.Button != MouseButtons.Left)
            //{
            //    return;
            //}
            _draggedButton = null;

            var btn = (MyButton)sender;

            if (_btnResizeInfo.CanResize)
            {
                _btnResizeInfo.StartPoint = e.Location;
                _btnResizeInfo.StartRect = btn.Bounds;
                _btnResizeInfo.IsResizing = true;
                _btnResizeInfo.PrevPoint = e.Location;

            }
            else
            {
                if(e.Button == MouseButtons.Right)
                {
                    _draggedButton = btn;
                    btn.DoDragDrop(btn, DragDropEffects.Move);
                }
            }
        }

        private void MyButton_MouseMove(object sender, MouseEventArgs e)
        {
            var btn = (MyButton)sender;

            if (e.Button == MouseButtons.Left && _btnResizeInfo.IsResizing)
            {
                var dx = e.X - _btnResizeInfo.StartPoint.X;
                var dy = e.Y - _btnResizeInfo.StartPoint.Y;

                switch (_btnResizeInfo.Direction)
                {
                    case ResizeDirection.Bottom:
                        if (_btnAddNew.Bottom >= this.ClientRectangle.Bottom && e.Y > _btnResizeInfo.PrevPoint.Y) //dragging down and reached bottom
                        {
                            _btnResizeInfo.IsResizing = false;
                        }

                        btn.Height = _btnResizeInfo.StartRect.Height + dy;

                        break;
                    case ResizeDirection.Right:
                        //btn.Width = _btnResizeInfo.StartRect.Width + dx;
                        break;
                }

                _btnResizeInfo.PrevPoint = e.Location;
                reArrangeButtons();
                _configChanged = true;
                return;
            }



            _btnResizeInfo.CanResize = false;
            _btnResizeInfo.IsResizing = false;
            _btnResizeInfo.Direction = ResizeDirection.None;
            btn.Cursor = Cursors.Default;


            if (_curBarEdge == Win32.AppBarEdge.ABE_LEFT || _curBarEdge == Win32.AppBarEdge.ABE_RIGHT)
            {
                if (e.Y > (btn.Height - _profile.ButtonResizeZoneWidth) && e.Y < btn.Height)
                {
                    _btnResizeInfo.CanResize = true;
                    _btnResizeInfo.Direction = ResizeDirection.Bottom;
                    btn.Cursor = Cursors.SizeNS;
                }
            }
            else if (_curBarEdge == Win32.AppBarEdge.ABE_TOP)
            {
                //if (e.X > (btn.Width - _profile.ButtonResizeZoneWidth) && e.X < btn.Width)
                //{
                //    _btnResizeInfo.CanResize = true;
                //    _btnResizeInfo.Direction = ResizeDirection.Right;
                //    btn.Cursor = Cursors.SizeWE;
                //}
            }

        }

        private void MyButton_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;

            if (_draggedButton != null)
            {
                var curButton = (MyButton)sender;

                if (curButton != null && _draggedButton != curButton)
                {
                    
                    _draggedButton.Top = curButton.Top;
                    curButton.Top = curButton.Top + 1;

                    reArrangeButtons();
                    
                    this.Refresh();
                    _configChanged = true;
                }

                _draggedButton = null;
               
            }

        }

        private void MyButton_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }


        private void loadProfile(string profileDir, string profileName)
        {

            var profileFileName = Path.Combine(profileDir, $"{profileName}.json");

            if (!File.Exists(profileFileName))
            {
                return;
            }

            using (var file = new StreamReader(profileFileName))
            {
                var jsonString = file.ReadToEnd();
                _profile = JsonConvert.DeserializeObject<Profile>(jsonString);
            }

        }


        private void buildMyButtonsFromCurrentProfile()
        {
            foreach (var btnConfig in _profile.ButtonConfigs)
            {
                var btn = new MyButton();

                btn.Id = btnConfig.Id;
                btn.ButtonType = btnConfig.Type;
                btn.ButtonText = btnConfig.ButtonText;
                btn.TooltipText = btnConfig.TooltipText;
                btn.Description = btnConfig.Description;
                btn.ForeColor = btnConfig.ForeColor;
                btn.BackColor = btnConfig.BackColor;
                btn.ButtonKeyCode = (Keys)btnConfig.KeyCode;
                btn.SliderUpKeyCode = (Keys)btnConfig.SliderUpKeyCode;
                btn.SliderDownKeyCode = (Keys)btnConfig.SliderDownKeyCode;
                btn.IsShift = btnConfig.IsShift;
                btn.IsCtrl = btnConfig.IsCtrl;
                btn.IsAlt = btnConfig.IsAlt;
                btn.Font = new Font(btnConfig.FontName, btnConfig.FontSize, FontStyle.Bold);
                btn.Left = btnConfig.Left;
                btn.Top = btnConfig.Top;
                btn.FlatStyle = FlatStyle.Popup;
                btn.ImageAlign = ContentAlignment.MiddleCenter;
                btn.ContextMenuStrip = _mnuButtonContext;
                btn.IconBase64 = btnConfig.IconBase64;


                if (!string.IsNullOrEmpty(btnConfig.IconBase64))
                {
                    btn.Image = ImageOps.Base64ToImage(btnConfig.IconBase64);
                }
                else if (!string.IsNullOrEmpty(btnConfig.ButtonText))
                {
                    btn.Text = btnConfig.ButtonText;
                }
                else
                {
                    btn.Image = Resources.Sad_32;
                }


                if (_curBarEdge == Win32.AppBarEdge.ABE_LEFT || _curBarEdge == Win32.AppBarEdge.ABE_RIGHT)
                {
                    btn.Width = this.ClientRectangle.Width;
                    btn.Height = btnConfig.Height;
                }
                else if (_curBarEdge == Win32.AppBarEdge.ABE_TOP)
                {
                    //btn.Width = btnConfig.Width;
                    //btn.Height = this.ClientRectangle.Height;
                }


                btn.Click += MyButton_Click;
                btn.PointerEnter += MyButton_PointerEnter;
                btn.PointerDown += MyButton_PointerDown;
                btn.PointerUpdate += MyButton_PointerUpdate;
                btn.PointerLeave += MyButton_PointerLeave;
                btn.MouseEnter += MyButton_MouseEnter;
                btn.MouseHover += MyButton_MouseHover;
                btn.PointerUp += MyButton_PointerUp;
                btn.MouseDown += MyButton_MouseDown;
                btn.MouseUp += MyButton_MouseUp;
                btn.MouseMove += MyButton_MouseMove;
                btn.MouseLeave += MyButton_MouseLeave;


                btn.AllowDrop = true;
               
                btn.DragOver += MyButton_DragOver;
                btn.DragDrop += MyButton_DragDrop;
                

                this.Controls.Add(btn);

            }

        }



        private bool saveProfile(string profileName)
        {

            if (!Directory.Exists(_profilesDir))
            {
                try
                {
                    Directory.CreateDirectory(_profilesDir);
                }
                catch (Exception ex)
                {
                    //log error
                    MessageBox.Show("Unexpected error while trying to create Profiles folder", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            string profileFileName;

            if (profileName.ToLower() == _defaultProfileName.ToLower())
            {
                profileFileName = Path.Combine(_defaultProfileDir, $"{profileName}.json");

            }
            else
            {
                profileFileName = Path.Combine(_profilesDir, $"{profileName}.json");

                if (File.Exists(profileFileName))
                {
                    if (MessageBox.Show("Another profile with the same name exists.\nDo you want to overwrite?", "Warning",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return false;
                    }
                }

            }


            var profile = new Profile
            {
                ProfileName = profileName,
                BarEdge = _profile.BarEdge,
                BarThickness = _profile.BarThickness,
                ButtonResizeZoneWidth = _profile.ButtonResizeZoneWidth,
                SliderResolution = _profile.SliderResolution,

            };


            //foreach (var btn in _buttons)
            List<MyButton> buttons;

            if (_curBarEdge == Win32.AppBarEdge.ABE_LEFT || _curBarEdge == Win32.AppBarEdge.ABE_RIGHT)
            {
                buttons = this.Controls.OfType<MyButton>().OrderBy(b => b.Top).ToList();
            }
            else if (_curBarEdge == Win32.AppBarEdge.ABE_TOP)
            {
                buttons = this.Controls.OfType<MyButton>().OrderBy(b => b.Left).ToList();
            }
            else
            {
                MessageBox.Show("Cannot save!\nFailed to determine the orientation of the toolbar", "Stop",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            foreach (var btn in buttons)

            {
                var btnConfig = new ButtonConfig();
                btnConfig.Id = btn.Id;
                btnConfig.Type = btn.ButtonType;
                btnConfig.ButtonText = btn.ButtonText;
                btnConfig.TooltipText = btn.TooltipText;
                btnConfig.Description = btn.Description;
                btnConfig.ForeColor = btn.ForeColor;
                btnConfig.BackColor = btn.BackColor;
                btnConfig.KeyCode = (int)btn.ButtonKeyCode;
                btnConfig.KeyName = btn.ButtonKeyCode.ToString();
                btnConfig.SliderUpKeyCode = (int)btn.SliderUpKeyCode;
                btnConfig.SliderDownKeyCode = (int)btn.SliderDownKeyCode;
                btnConfig.SliderUpKeyName = btn.SliderUpKeyCode.ToString();
                btnConfig.SliderDownKeyName = btn.SliderDownKeyCode.ToString();
                btnConfig.IsShift = btn.IsShift;
                btnConfig.IsCtrl = btn.IsCtrl;
                btnConfig.IsAlt = btn.IsAlt;
                btnConfig.FontName = btn.Font.Name;
                btnConfig.FontSize = btn.Font.Size;
                btnConfig.Left = btn.Left;
                btnConfig.Top = btn.Top;
                btnConfig.Width = btn.Width;
                btnConfig.Height = btn.Height;
                btnConfig.IconBase64 = btn.IconBase64;

                profile.ButtonConfigs.Add(btnConfig);

            }


            try
            {
                string jsonString = JsonConvert.SerializeObject(profile);

                using (var file = new StreamWriter(profileFileName))
                {
                    file.WriteLine(jsonString);
                }

                return true;

            }
            catch (Exception ex)
            {
                //log error
                MessageBox.Show("Unexpected error while trying to save profile", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

        }



        private void setupAddNewButton()
        {
            Button btn;

            if (!this.Controls.Contains(_btnAddNew))
            {
                btn = new Button();
                btn.Click += BtnAddNew_Click;
                btn.MouseEnter += (s, e) => { toolTip.Show("Add new btn", btn, 1000); };
                btn.MouseHover += (s, e) => { toolTip.Show("Add new btn", btn, 1000); };
                btn.ImageAlign = ContentAlignment.MiddleCenter;
                btn.Image = Resources.add_32;
                btn.FlatStyle = FlatStyle.Popup;
                btn.ForeColor = Color.Black;
                btn.Font = new Font("Segoe UI", 15, FontStyle.Bold);
                btn.BackColor = Color.FromArgb(100, 100, 100);



                this.Controls.Add(btn);
                _btnAddNew = btn;
            }
            else
            {
                btn = _btnAddNew;
            }


            if (_curBarEdge == Win32.AppBarEdge.ABE_LEFT || _curBarEdge == Win32.AppBarEdge.ABE_RIGHT)
            {
                btn.Left = this.ClientRectangle.Left;
                btn.Width = this.ClientRectangle.Width;
                btn.Height = btn.Width;

                var prevButton = this.Controls.OfType<MyButton>().OrderBy(b => b.Top).LastOrDefault();

                if (prevButton != null)
                {
                    btn.Top = prevButton.Top + prevButton.Height;
                }
                else
                {
                    btn.Top = _btnMenu.Top + _btnMenu.Height;
                }

            }
            else if (_curBarEdge == Win32.AppBarEdge.ABE_TOP)
            {
                //btn.Width = 30;
                //btn.Height = this.ClientRectangle.Height;
                //btn.Left = this.ClientRectangle.Right - btn.Width;
                //btn.Top = this.ClientRectangle.Top;
            }

        }

        private void clearControls()
        {
            while (this.Controls.Count > 0)
            {
                foreach (Control ctl in this.Controls)
                {
                    this.Controls.Remove(ctl);
                    ctl.Dispose();

                }
            }

        }

        private void clearMyButtons()
        {
            while (this.Controls.OfType<MyButton>().Count() > 0)
            {
                foreach (MyButton btn in this.Controls.OfType<MyButton>())
                {
                    this.Controls.Remove(btn);
                    btn.Dispose();

                }
            }

        }


        private void setupMenuButton()
        {
            var btn = new Button();
            btn.Text = "=";

            btn.MouseDown += (s, e) =>
            {
                var p = this.PointToScreen(e.Location);
                _btnMenu.ContextMenuStrip.Show(btn, p);
            };

            btn.FlatStyle = FlatStyle.Popup;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btn.BackColor = Color.FromArgb(255, 100, 100);
            btn.MouseEnter += (s, e) => { toolTip.Show("Menu", btn, 1000); };
            btn.MouseHover += (s, e) => { toolTip.Show("Menu", btn, 1000); };



            if (_curBarEdge == Win32.AppBarEdge.ABE_LEFT || _curBarEdge == Win32.AppBarEdge.ABE_RIGHT)
            {
                btn.Left = this.ClientRectangle.Left;
                btn.Top = this.ClientRectangle.Top;
                btn.Width = this.ClientRectangle.Width;
                btn.Height = btn.Width;
            }
            else if (_curBarEdge == Win32.AppBarEdge.ABE_TOP)
            {
                //btn.Width = 30;
                //btn.Height = this.ClientRectangle.Height;
                //btn.Left = this.ClientRectangle.Right - btn.Width;
                //btn.Top = this.ClientRectangle.Top;
            }

            btn.ContextMenuStrip = _mnuMainContext;
            this.Controls.Add(btn);

            _btnMenu = btn;
        }


        private void setupMainContextMenu()
        {
            var mnuContext = new ContextMenuStrip();
            mnuContext.Items.Add("Close", null, (s, e) => { this.Close(); });
            //------------------------------------------------
            mnuContext.Items.Add("-");
            var mnuLoadProfiles = new ToolStripMenuItem("Load profile");
            mnuContext.Items.Add(mnuLoadProfiles);

            var mnuDeleteProfiles = new ToolStripMenuItem("Delete profile");
            mnuContext.Items.Add(mnuDeleteProfiles);

            mnuContext.Items.Add("Save Profile As...", null, (s, e) => { saveProfile(); });
            mnuContext.Items.Add("Save Profile As Default", null, (s, e) => { saveProfileAsDefault(); });
            //------------------------------------------------
            mnuContext.Items.Add("-");
            var mnuToolbarSize = new ToolStripMenuItem("Size");
            var smallMenuItem = mnuToolbarSize.DropDownItems.Add("Small", null, (s, e) => { switchBarSize((int)BarThickness.Small); });
            var mediumMenuItem = mnuToolbarSize.DropDownItems.Add("Medium", null, (s, e) => { switchBarSize((int)BarThickness.Medium); });
            var largeMenuItem = mnuToolbarSize.DropDownItems.Add("Large", null, (s, e) => { switchBarSize((int)BarThickness.Large); });

            ((ToolStripMenuItem)smallMenuItem).Checked = this.Width == (int)BarThickness.Small;
            ((ToolStripMenuItem)mediumMenuItem).Checked = this.Width == (int)BarThickness.Medium;
            ((ToolStripMenuItem)largeMenuItem).Checked = this.Width == (int)BarThickness.Large;
            mnuContext.Items.Add(mnuToolbarSize);
            //------------------------------------------------
            mnuContext.Items.Add("-");
            var mnuToolbarPosition = new ToolStripMenuItem("Position");
            var leftPosMenuItem = mnuToolbarPosition.DropDownItems.Add("Left", null, (s, e) => { switchBarEdgePos(Win32.AppBarEdge.ABE_LEFT); });
            var rightPosMenuItem = mnuToolbarPosition.DropDownItems.Add("Right", null, (s, e) => { switchBarEdgePos(Win32.AppBarEdge.ABE_RIGHT); });

            ((ToolStripMenuItem)leftPosMenuItem).Checked = this._curBarEdge == Win32.AppBarEdge.ABE_LEFT;
            ((ToolStripMenuItem)rightPosMenuItem).Checked = this._curBarEdge == Win32.AppBarEdge.ABE_RIGHT;
            mnuContext.Items.Add(mnuToolbarPosition);
            //------------------------------------------------
            mnuContext.Items.Add("-");
            mnuContext.Items.Add("Remove All Buttons", null, (s, e) => { removeAllButtons(); });
            //------------------------------------------------
            mnuContext.Items.Add("-");
            _mnuTimeLapase = mnuContext.Items.Add("Start Screen Recording...", null, (s, e) => { recordTimeLapse(); });

            //------------------------------------------------
            mnuContext.Items.Add("-");
            mnuContext.Items.Add("About...", null, (s, e) => 
            {
                using (var frm = new frmAbout())
                {
                    frm.ShowDialog(this);
                }
            });


            _mnuMainContext = mnuContext;
            _mnuLoadProfiles = mnuLoadProfiles;
            _mnuDeleteProfiles = mnuDeleteProfiles;

            populateProfilesToMenu();
        }


        private void recordTimeLapse()
        {

            try
            {
                if (_timeLapeStarted)
                {
                    WindowShot.StopRecording();
                    _timeLapeStarted = false;
                    _mnuTimeLapase.Text = "Start Screen Recording...";

                }
                else
                {
                    using (var frm = new frmWindowShot())
                    {
                        frm.VideosFolder = _videosFolder;
                        frm.ShowDialog(this);

                        if (frm.DialogResult == DialogResult.OK && !string.IsNullOrEmpty(frm.ProcessName))
                        {
                            WindowShot.StartRecording(frm.ProcessName, _videosFolder);
                            _mnuTimeLapase.Text = "Stop Screen Recording";
                            _timeLapeStarted = true;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected Error:" + ex.ToString(), "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }            

        }


        private void populateProfilesToMenu()
        {

            _mnuLoadProfiles.DropDownItems.Clear();
            _mnuDeleteProfiles.DropDownItems.Clear();

            Directory.GetFiles(_profilesDir, "*.json").ToList().ForEach(f =>
            {
                var profileName = Path.GetFileNameWithoutExtension(f);

                _mnuLoadProfiles.DropDownItems.Add(profileName, null, (s, e) =>
                {
                    loadProfile(_profilesDir, profileName);
                    clearMyButtons();
                    buildMyButtonsFromCurrentProfile();
                    reArrangeButtons();
                    _configChanged = true;
                });

                _mnuDeleteProfiles.DropDownItems.Add(profileName, null, (s, e) =>
                {
                    if (MessageBox.Show($"Do you want to delete profile {profileName}?", "Delete Profile",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            File.Delete(f);
                            populateProfilesToMenu();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unexpected error while trying to delete the profile", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                });

            });



        }

        private void removeAllButtons()
        {
            clearMyButtons();
            reArrangeButtons();
            _configChanged = true;
        }

        private void saveProfile()
        {

            using (var frm = new frmSaveProfile())
            {
                if (frm.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                saveProfile(frm.ProfileName);
                populateProfilesToMenu();
            }
        }

        private void switchBarSize(int size)
        {
            _profile.BarThickness = size;
            _configChanged = true;

            MessageBox.Show("New size will take effect on application startup next time.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void switchBarEdgePos(Win32.AppBarEdge edge)
        {
            _profile.BarEdge = edge;
            _configChanged = true;

            MessageBox.Show("New position will take effect on application startup next time.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            //removeAppBar();
            //setupNewAppBar();
        }




        private void setupButtonContextMenu()
        {
            var mnuContext = new ContextMenuStrip();
            mnuContext.Items.Add("Remove Button", null, (s, e) =>
            {
                var btn = ((s as ToolStripItem).Owner as ContextMenuStrip).SourceControl as MyButton;
                this.Controls.Remove(btn);
                btn.Dispose();

                reArrangeButtons();

            });


            mnuContext.Items.Add("Edit Button", null, (s, e) =>
            {
                var btn = ((s as ToolStripItem).Owner as ContextMenuStrip).SourceControl as MyButton;
                editMyButton(btn);
            });

            _mnuButtonContext = mnuContext;

        }


        private void editMyButton(MyButton btn)
        {
            using (var frm = new frmAddEditMyButton())
            {
                frm.IsEditMode = true;
                frm.ButtonId = btn.Id;
                frm.ButtonType = btn.ButtonType;
                frm.ButtonText = btn.ButtonText;
                frm.TooltipText = btn.TooltipText;
                frm.Description = btn.Description;
                frm.TextColor = btn.ForeColor;
                frm.BackgroundColor = btn.BackColor;
                frm.PushButtonKeyCode = btn.ButtonKeyCode;
                frm.SliderUpKeyCode = btn.SliderUpKeyCode;
                frm.SliderDownKeyCode = btn.SliderDownKeyCode;
                frm.IsShift = btn.IsShift;
                frm.IsCtrl = btn.IsCtrl;
                frm.IsAlt = btn.IsAlt;
                frm.IconBase64 = btn.IconBase64;

                if (frm.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                btn.ButtonType = frm.ButtonType;
                btn.ButtonText = frm.ButtonText;
                btn.TooltipText = frm.TooltipText;
                btn.Description = frm.Description;
                btn.ForeColor = frm.TextColor;
                btn.BackColor = frm.BackgroundColor;
                btn.ButtonKeyCode = frm.PushButtonKeyCode;
                btn.SliderUpKeyCode = frm.SliderUpKeyCode;
                btn.SliderDownKeyCode = frm.SliderDownKeyCode;
                btn.IsShift = frm.IsShift;
                btn.IsCtrl = frm.IsCtrl;
                btn.IsAlt = frm.IsAlt;
                btn.IconBase64 = frm.IconBase64;

                if (!string.IsNullOrEmpty(frm.IconBase64))
                {
                    btn.Image = ImageOps.Base64ToImage(frm.IconBase64);
                }
                else if (!string.IsNullOrEmpty(frm.ButtonText))
                {
                    btn.Text = frm.ButtonText;
                }
                else
                {
                    btn.Image = Resources.Sad_32;
                }



                _configChanged = true;

            }

        }


        private void saveProfileAsDefault()
        {
            if (saveProfile(_defaultProfileName))
                _configChanged = false;
        }


        private void reArrangeButtons()
        {

            var myButtons = this.Controls.OfType<MyButton>().OrderBy(b => b.Top).Distinct().ToList();
            //var myButtons = this.Controls.OfType<MyButton>().OrderBy(b => this.Controls.GetChildIndex(b)).ToList();

            for (int i = 0; i < myButtons.Count; i++)
            {
                if (_curBarEdge == Win32.AppBarEdge.ABE_LEFT || _curBarEdge == Win32.AppBarEdge.ABE_RIGHT)
                {
                    if (i == 0)
                    {
                        myButtons[i].Top = _btnMenu.Top + _btnMenu.Height;
                    }
                    else
                    {
                        myButtons[i].Top = myButtons[i - 1].Top + myButtons[i - 1].Height;
                    }
                }
                else if (_curBarEdge == Win32.AppBarEdge.ABE_TOP)
                {

                }
            }



            setupAddNewButton();
        }


        protected override void WndProc(ref Message m)
        {
            switch ((Win32.WindowMessages)m.Msg)
            {
                case Win32.WindowMessages.APPBAR_CALLBACK_CUSTOM_MSG:
                    if ((Win32.AppBarNotification)m.WParam.ToInt32() == Win32.AppBarNotification.ABN_POSCHANGED)
                    {
                        frmMain_AppBarPositionChanged(_appBarInfo);
                    }
                    break;
                case Win32.WindowMessages.WM_DISPLAYCHANGE:
                    frmMain_DisplayChanged(Win32.LoWord(m.WParam.ToInt32()), Win32.LoWord(m.LParam.ToInt32()), Win32.HiWord(m.LParam.ToInt32()));
                    break;
                default:
                    break;
            }


            base.WndProc(ref m);
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
        }
    }
}
