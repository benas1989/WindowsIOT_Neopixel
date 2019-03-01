using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                System.Diagnostics.Debug.WriteLine("InitializeAsync() error: {0}", ex.Message);
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

        public async Task ShowAsync()
        {
            try
            {
                if (!initialiseComplete)
                    return;
                spi.Write(pixelsBuffer);
                await Task.Delay(1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ShowAsync() error: {0}", ex.Message);
            }
            return;
        }

    }
}
