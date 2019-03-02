using Neopixel.Colors;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Spi;

namespace Neopixel
{
    public class WS2812B
    {
        private const string SPI_PORT_NAME = "SPI1";
        private int totalPixels;
        private byte[] pixelsBuffer;
        private SpiDevice spi;
        private bool initialiseComplete = false;

        private RGBColor GetColorFromHex(string hex)
        {
            try
            {
                hex = hex.Replace("#", string.Empty);
                byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
                byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
                byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
                return new RGBColor(r,g,b);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetSolidColorBrusch() error: {0}", ex.Message);
            }
            return null;
        }

        public void ClearPixelsBuffer()
        {
            for (int i = 0; i < totalPixels*3; i++)
            {
                pixelsBuffer[i] = 0;
            }
        }

        public async Task<bool> InitializeAsync(int pixels)
        {
            try
            {
                var settings = new SpiConnectionSettings(0);
                settings.ClockFrequency = 1000000;
                settings.Mode = SpiMode.Mode1;
                settings.DataBitLength = 8;
                string spiAqs = SpiDevice.GetDeviceSelector(SPI_PORT_NAME);
                var devicesInfo = await DeviceInformation.FindAllAsync(spiAqs);
                spi = await SpiDevice.FromIdAsync(devicesInfo[0].Id, settings);
                pixelsBuffer = new byte[pixels * 3];
                totalPixels = pixels;
                ClearPixelsBuffer();
                initialiseComplete = true;
                await ShowAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("InitializeAsync() error: {0}", ex.Message);
                return false;
            }
            return true;
        }

        public bool SetPixel(int position, byte red, byte green, byte blue)
        {
            if (position >= totalPixels)
                return false;
            int startPosition = position * 3;
            pixelsBuffer[startPosition] = green;
            pixelsBuffer[startPosition + 1] = red;
            pixelsBuffer[startPosition + 2] = blue;
            return true;
        }

        public bool SetPixel(int position, string hex)
        {
            var color = GetColorFromHex(hex);
            if (color != null)
            {
                return SetPixel(position, color.Red,color.Green, color.Blue);
            }
            return false;
        }

        public bool SetPixel(int position, RGBColor color)
        {
            if (color != null)
            {
                return SetPixel(position, color.Red, color.Green, color.Blue);
            }
            else
            {
                return false;
            }
        }

        public bool SetPixel(int position,HSLColor color)
        {
            if (color != null)
            {
                return SetPixel(position, color.Red, color.Green, color.Blue);
            }
            else
            {
                return false;
            }
        }

        public async Task ShowAsync()
        {
            try
            {
                if (!initialiseComplete)
                    return;
                spi.Write(pixelsBuffer);
                await Task.Delay(2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ShowAsync() error: {0}", ex.Message);
            }
            return;
        }

    }
}
