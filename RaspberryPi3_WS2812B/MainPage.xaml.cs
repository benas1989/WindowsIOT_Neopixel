using Neopixel;
using Neopixel.Colors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RaspberryPi3_WS2812B
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        WS2812B strip;
        public MainPage()
        {
            this.InitializeComponent();




            HSLColor cp = new HSLColor(100, 69, 28);
            RGBColor c = new RGBColor(cp.Red, cp.Green, cp.Blue);
            var x = c.test();




            Task.Factory.StartNew(() => { Run(); });
        }

        async void Run()
        {
            int pixels = 104;
            strip = new WS2812B();
            await strip.InitializeAsync(pixels);
            while (true)
            {
                int hue = 0;
                for (int i = 0; i < pixels; i++)
                {
                    var color = new HSLColor(hue, 100, 40);
                    strip.SetPixel(i,color.Red,color.Green,color.Blue);
                    await strip.ShowAsync();
                    hue = hue + 3;
                }
                hue = 0;
                strip.ClearPixelsBuffer();
                for (int i = pixels-1; i >= 0; i--)
                {
                    var color = new HSLColor(hue, 100, 40);
                    strip.SetPixel(i, color.Red, color.Green, color.Blue);
                    await strip.ShowAsync();
                    hue = hue + 3;
                }
                hue = 0;
                strip.ClearPixelsBuffer();
            }
        }
    }
}
