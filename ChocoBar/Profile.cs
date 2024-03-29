using System.Collections.Generic;

namespace ChocoBar
{
    internal class Profile
    {
        public Profile()
        {
            ButtonConfigs = new List<ButtonConfig>();
        }

        public string ProfileName { get; set; }

        public int SliderResolution { get; set; }

        public List<ButtonConfig> ButtonConfigs { get; set; }

        public Win32.AppBarEdge BarEdge { get; set; }

        public int BarThickness { get; set; }
        public int ButtonResizeZoneWidth { get; set; }
    }
}
