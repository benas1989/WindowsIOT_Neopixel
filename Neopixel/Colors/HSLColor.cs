using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neopixel.Colors
{
   public class HSLColor : RGBColor
    {
        private int hue;
        private byte saturation;
        private byte luminance;
        public int Hue
        {
            get
            {
                return hue;
            }
            set
            {
                if (value > 360)
                {
                    hue = 360;
                }
                else if(value < 0)
                {
                    hue = 0;
                }
                else
                {
                    hue = value;
                }
                UpdateRGBValues();
            }
        }
        public byte Saturation
        {
            get
            {
                return saturation;
            }
            set
            {
                if (value > 100)
                {
                    saturation = 100;
                }
                else
                {
                    saturation = value;
                }
                UpdateRGBValues();
            }
        }
        public byte Luminance
        {
            get
            {
                return luminance;
            }
            set
            {
                if (value > 100)
                {
                    luminance = 100;
                }
                else
                {
                    luminance = value;
                }
                UpdateRGBValues();
            }
        }

        private void UpdateRGBValues()
        {
            var color = ColorConverter.FromHSL(hue, saturation, luminance);
            Red = color.Red;
            Green = color.Green;
            Blue = color.Blue;
        }

        public HSLColor(int hue,byte saturation,byte luminance)
        {
            this.hue = hue;
            this.saturation = saturation;
            this.luminance = luminance;
            UpdateRGBValues();
        }
    }
}
