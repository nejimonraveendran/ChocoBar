using System.Drawing;

namespace ChocoBar
{
    internal enum ResizeDirection
    {
        None = 0,
        Right,
        Bottom,
    }

    internal class ButtonResizeInfo
    {
        public bool IsResizing { get; set; }
        public bool CanResize { get; set; }
        public ResizeDirection Direction { get; set; }
        public Point StartPoint { get; set; }
        public Point PrevPoint { get; set; }
        public Rectangle StartRect { get; set; }

    }
}
