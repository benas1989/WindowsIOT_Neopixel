using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neopixel.Colors
{
    public class RGBColor
    {
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }

        public RGBColor(byte red,byte green,byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public RGBColor()
        {

        }

        public HSLColor test()
        {
            return ColorConverter.FromRGB(Red,Green,Blue);
        }
    }
}
