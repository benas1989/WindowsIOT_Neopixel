using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neopixel.Colors
{
   public class ColorConverter
    {

        public static HSLColor FromRGB(byte red,byte green,byte blue)
        {
            double tmpRed = (red / 255d);
            double tmpGreen = (green / 255d);
            double tmpBlue = (blue / 255d);
            double min = Math.Min(Math.Min(tmpRed,tmpGreen), tmpBlue);
            double max = Math.Max(Math.Max(tmpRed, tmpGreen), tmpBlue);
            byte luminance = Convert.ToByte(((max + min) / 2.0d) * 100.0d);
            byte saturation = 0;
            double hue = 0d;
            if (min != max)
            {
                if (luminance < 50)
                {
                    saturation = Convert.ToByte(((max - min) / (max + min)) * 100d);
                }
                else
                {
                    saturation = Convert.ToByte(((max - min) / (2.0d - max + min)) * 100d);
                }


                if (max == tmpRed)
                {
                    hue = (tmpGreen - tmpBlue) / (max - min);
                }
                else if (max == tmpGreen)
                {
                    hue = 2.0d + (tmpBlue - tmpRed) / (max - min);
                }
                else
                {
                    hue = 4.0d + (tmpRed - tmpGreen) / (max - min);
                }
                hue = hue * 60.0d;
                if (hue < 0)
                {
                    hue = hue + 360d;
                }
            }
            else
            {
                hue = 0;
                saturation = 0;
            }
            return new HSLColor((int)hue,saturation,luminance);
        }

        public static RGBColor FromHSL(int hue,byte saturation,byte luminance)
        {
            RGBColor color = new RGBColor(0, 0, 0);
            double tmpSaturation = saturation / 100d;
            double tmpLuminance = luminance/ 100d;
            double tmpHue = 0.0d;


            if (saturation == 0)
            {
                color.Red = Convert.ToByte(tmpLuminance * 255.0d);
                color.Green = Convert.ToByte(tmpLuminance * 255.0d);
                color.Blue = Convert.ToByte(tmpLuminance * 255.0d);
            }
            else
            {
                double tmp1 = 0d;
                double tmp2 = 0d;
                double tmpRed, tmpBlue, tmpGreen;

                if (luminance < 50)
                {
                    tmp1 = tmpLuminance * (1.0f + tmpSaturation);
                }
                else
                {
                    tmp1 = tmpLuminance + tmpSaturation - (tmpLuminance * tmpSaturation);
                }
                tmp2 = 2 * tmpLuminance - tmp1;
                tmpHue = hue / 360.0f;
                tmpRed = tmpHue + 0.333f;
                tmpGreen = tmpHue;
                tmpBlue = tmpHue - 0.333f;
                color.Red = CalculateColor(tmpRed, tmp1, tmp2);
                color.Green = CalculateColor(tmpGreen, tmp1, tmp2);
                color.Blue = CalculateColor(tmpBlue, tmp1, tmp2);
            }
            return color;
        }

        private static byte CalculateColor(double color,double tmp1,double tmp2)
        {
            if (color > 1)
            {
                color = color - 1d;
            }
            else if (color < 0)
            {
                color = color + 1;
            }


            if ((color * 6.0d) < 1.0d)
            {
                color = tmp2 + (tmp1 - tmp2) * 6 * color;
            }
            else if ((color * 2.0d) < 1.0d)
            {
                color = tmp1;
            }
            else if ((color * 3.0d) < 2.0d)
            {
                color = tmp2 + (tmp1 - tmp2) * (0.666d - color) * 6.0d;
            }
            else
            {
                color = tmp2;
            }
            return Convert.ToByte(color * 255);
        }

    }
}
