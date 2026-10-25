using System;
using System.IO;
using Windows.Devices.Usb;
using System.Text;

namespace WinUSB_WinFormsApp
{
    public partial class Form1 : Form
    {
        UsbDevice usbDevice;

        private async void connectDevice()
        {
            UInt32 vid = 0xC251;
            UInt32 pid = 0x4507;

            string aqs = UsbDevice.GetDeviceSelector(vid, pid);

            var myDevices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(aqs);

            try
            {
                usbDevice = await UsbDevice.FromIdAsync(myDevices[0].Id);
            }
            catch (Exception exception)
            {
                connectLabel.Text = exception.Message.ToString();
            }
            finally
            {
                connectLabel.Text = "Opened device for communication." + ((usbDevice != null) ? myDevices[0].Name : "Warning usbDevice is Null");
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectDevice();

        }

        private async Task BulkWrite(byte[] dataBytes)
        {
            UsbBulkOutPipe writePipe = usbDevice.DefaultInterface.BulkOutPipes[0];
            writePipe.WriteOptions |= UsbWriteOptions.ShortPacketTerminate;

            try
            {
                Stream outputStream = writePipe.OutputStream.AsStreamForWrite();
                await outputStream.WriteAsync(dataBytes, 0, dataBytes.Length);
                await outputStream.FlushAsync();
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
        }


        private async Task<byte[]> BulkRead()
        {
            UsbBulkInPipe readPipe = usbDevice.DefaultInterface.BulkInPipes[0];
            byte[] buffer = new byte[readPipe.EndpointDescriptor.MaxPacketSize];

            try
            {
                Stream inputStream = readPipe.InputStream.AsStreamForRead();
                int bytesRead = await inputStream.ReadAsync(buffer, 0, buffer.Length);
                return buffer[..bytesRead].ToArray();
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
                return Array.Empty<byte>();
            }
        }

        private async void transferButton_Click(object sender, EventArgs e)
        {
            await BulkWrite(Encoding.UTF8.GetBytes(transferTextBox.Text));
            byte[] receivedData = await BulkRead();
            transferLabel.Text = Encoding.UTF8.GetString(receivedData);
        }
    }
}
