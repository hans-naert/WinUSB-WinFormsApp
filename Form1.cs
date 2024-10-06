using System.Collections;
using System;
using Windows.Devices.Usb;
using Windows.Storage.Streams;
using System.Text;
using System.Security.Cryptography;

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
            UInt32 bytesWritten = 0;

            UsbBulkOutPipe writePipe = usbDevice.DefaultInterface.BulkOutPipes[0];
            writePipe.WriteOptions |= UsbWriteOptions.ShortPacketTerminate;

            var stream = writePipe.OutputStream;

            DataWriter writer = new DataWriter(stream);

            writer.WriteBytes(dataBytes);

            try
            {
                bytesWritten = await writer.StoreAsync();
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message.ToString());
            }
            finally
            {
                Console.Write("Data written: " + bytesWritten + " bytes.");
            }
        }


        private async Task<byte[]> BulkRead()
        {
            UInt32 bytesRead = 0;
            byte[] bytes;

            UsbBulkInPipe readPipe = usbDevice.DefaultInterface.BulkInPipes[0];
            var stream = readPipe.InputStream;
            DataReader reader = new DataReader(stream);

            try
            {
                bytesRead = await reader.LoadAsync(readPipe.EndpointDescriptor.MaxPacketSize);
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message.ToString());
            }
            finally
            {
                IBuffer buffer = reader.ReadBuffer(bytesRead);
                var dataReader = DataReader.FromBuffer(buffer);
                bytes = new byte[bytesRead];
                dataReader.ReadBytes(bytes);                
            }

            return bytes;
        }

        private async void transferButton_Click(object sender, EventArgs e)
        {
            await BulkWrite(Encoding.UTF8.GetBytes(transferTextBox.Text));
            byte[] receivedData = await BulkRead();
            transferLabel.Text = Encoding.UTF8.GetString(receivedData);
        }
    }
}
