using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChocoBar
{
    public enum ButtonId
    {
        btnClose,
        btnSize,
        btnUndo,
        btnTablet,
        btnDry,
        btnFastDry,
        btnPaint,
        btnBlend,
        btnErase,
        btnLoadingOpacity,
        btnWaterOiliness,
        btnPressure,
    }

    public enum ButtonType
    {
        Push = 1,
        Slider = 2,
    }


    public class PointerEventArgs : EventArgs
    {
        public int PointerId { get; set; }
        public bool IsInContact { get; set; }
        public bool IsInRange { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }



    public class MyButton : Button
    {
        public event EventHandler<PointerEventArgs> PointerDown;
        public event EventHandler<PointerEventArgs> PointerUp;
        public event EventHandler<PointerEventArgs> PointerEnter;
        public event EventHandler<PointerEventArgs> PointerLeave;
        public event EventHandler<PointerEventArgs> PointerUpdate;


        public Guid Id { get; set; }
        //public string IconPath { get; set; }

        public ButtonType ButtonType { get; set; }
        public string ButtonText { get; set; }
        public string TooltipText { get; set; }
        public Keys ButtonKeyCode { get; set; }
        public Keys SliderUpKeyCode { get; set; }
        public Keys SliderDownKeyCode { get; set; }
        public bool IsShift { get; set; }
        public bool IsCtrl { get; set; }
        public bool IsAlt { get; set; }
        public string Description { get; set; }
        public string IconBase64 { get; set; }

        protected override void WndProc(ref Message m)
        {
            if (Win32.IsPointerMessage(m))
            {
                var pointerEventArgs = new PointerEventArgs
                {
                    PointerId = Win32.GetPointerId(m),
                    IsInContact = Win32.IsPointerInContact(m),
                    IsInRange = Win32.IsPointerInRange(m)
                };

                var point = this.PointToClient(new Point(Win32.LoWord(m.LParam.ToInt32()), Win32.HiWord(m.LParam.ToInt32())));
                pointerEventArgs.X = point.X;
                pointerEventArgs.Y = point.Y;

                switch ((Win32.WindowMessages)m.Msg)
                {
                    case Win32.WindowMessages.WM_POINTERDOWN:
                        if (PointerDown != null)
                        {
                            PointerDown(this, pointerEventArgs);
                        }
                        break;
                    case Win32.WindowMessages.WM_POINTERUP:
                        if (PointerUp != null)
                        {
                            PointerUp(this, pointerEventArgs);
                        }
                        break;
                    case Win32.WindowMessages.WM_POINTERENTER:
                        if (PointerEnter != null)
                        {
                            PointerEnter(this, pointerEventArgs);
                        }
                        break;
                    case Win32.WindowMessages.WM_POINTERLEAVE:
                        if (PointerLeave != null)
                        {
                            PointerLeave(this, pointerEventArgs);
                        }
                        break;
                    case Win32.WindowMessages.WM_POINTERUPDATE:
                        if (PointerUpdate != null)
                        {
                            PointerUpdate(this, pointerEventArgs);
                        }
                        break;
                    default:
                        break;
                }

            }

            base.WndProc(ref m);
        }
    }
}
