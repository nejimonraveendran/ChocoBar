using System;
using System.Drawing;

namespace ChocoBar
{
    internal class ButtonConfig
    {
        public Guid Id { get; set; }
        public string ButtonText { get; set; }
        public string TooltipText { get; set; }
        //public string IconPath { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsShift { get; set; }
        public bool IsCtrl { get; set; }
        public bool IsAlt { get; set; }
        public string KeyName { get; set; }
        public int KeyCode { get; set; }
        public int SliderUpKeyCode { get; set; }
        public int SliderDownKeyCode { get; set; }
        public string SliderUpKeyName { get; set; }
        public string SliderDownKeyName { get; set; }
        public ButtonType Type { get; set; }
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        public float FontSize { get; set; }
        public string FontName { get; set; }
        public string Description { get; set; }
        public string IconBase64 { get; set; }
    }
}
