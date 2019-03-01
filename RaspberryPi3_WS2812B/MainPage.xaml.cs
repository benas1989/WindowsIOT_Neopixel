using Neopixel;
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
            Task.Factory.StartNew(() => { Run(); });
        }

        async void Run()
        {
            int pixels = 104;
            strip = new WS2812B();
            await strip.InitializeAsync(pixels);
            while (true)
            {
                strip.ClearPixelsBuffer();
                for (int i = 0; i < pixels; i++)
                {
                    strip.SetPixel(i, 50,0,0);
                    await strip.ShowAsync();
                }
                await Task.Delay(500);
                for (int i = 0; i < pixels; i++)
                {
                    strip.SetPixel(i, 0, 50, 0);
                    await strip.ShowAsync();
                }
                await Task.Delay(500);
                for (int i = 0; i < pixels; i++)
                {
                    strip.SetPixel(i, 0, 0, 50);
                    await strip.ShowAsync();
                }
                await Task.Delay(500);
            }
        }
    }
}
