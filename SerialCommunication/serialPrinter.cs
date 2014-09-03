using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace SerialCOM
{
    
    public class SerialPrinter
    {
        
        internal SerialPort myComPort;
        public string lastError;

        public SerialPrinter() //Constructor
        {
            myComPort = new SerialPort();
            if (!(myComPort.IsOpen))
            {
                myComPort.BaudRate = 115200;
                //myComPort.PortName = portName;
                myComPort.Parity = Parity.None;
                myComPort.DataBits = 8;
                myComPort.StopBits = StopBits.One;
            }
        }
        public void open(string portName)
        {
            if (!(myComPort.IsOpen))
            {
                try
                {
                    myComPort.PortName = portName;
                    myComPort.Open();
                }
                catch (UnauthorizedAccessException ex)
                {
                    setError(ex.Message);
                    throw new Exception("Acceso no autorizado");
                }
            }
        }
        public void close()
        {
            if (myComPort.IsOpen)
            {
                myComPort.Dispose();
                myComPort.Close();
            }
        }

        public string showError()
        {
            return lastError;
        }
        private void setError(string error)
        {
            lastError = error;
        }
        public void WriteLine(string ASCII)
        {
            myComPort.WriteLine(ASCII);
        }
        public void writeByte(Byte[] buffer, int count)
        {
            for (int i = 0; i < count; i++)
            {
                myComPort.Write(buffer[i].ToString());
            }
        }
        public void sendCommand(Byte[] buffer)
        {
            myComPort.Write(buffer, 1, buffer[0]);
        }
        
    }
    public class ESCPrinter :SerialPrinter
    {
        public ESCPrinter() { }
        public static byte ESC { get { return 0x1B; } }
        public static byte HT { get { return 0x09; } }
        public static byte LF { get { return 0x0A; } }
        public static byte CR { get { return 0x0D; } }
        public static byte dash { get { return 0x2D; } }
        public static byte at { get { return 0x40; } }
        public static byte E { get { return 0x45; } }
        public static byte G { get { return 0x47; } }
        public static byte M { get { return 0x4D; } }
        public static byte GS { get { return 0x1D; } }
        public static byte EMPH = 8;
        public static byte DWIDTH = 32;
        public static byte DHEIGHT = 16;
        public static byte UNDER = 128;

        public void lineSpacing()
        {
            Byte[] command = { 3, ESC, 0x33,0x00 };
            this.sendCommand(command);
        }
        public void lineSpacing(byte n)
        {
            Byte[] command = { 3, ESC, 0x33, n };
            this.sendCommand(command);
        }
        public void printMode(byte modes)
        {
            Byte[] command = { 3, ESC, 0x21, modes };
            this.sendCommand(command);
        }
        
        public void barcodeEAN13(string barcode)
        {
            Byte[] header = { GS, 0x6B, 2 };
            Byte[] barcodeArray = Encoding.ASCII.GetBytes(barcode);
            Byte[] nulling = { 0x00 };
            Byte[] command = join(header, barcodeArray);
            command = join(command, nulling);
            Byte[] length = {(byte)command.Length};
            command = join(length, command);
            this.sendCommand(command);

        }
        public void barcode128(string barcode)
        {
            Byte[] header = { GS, 0x6B, 72 };
            Byte[] barcodeArray = Encoding.ASCII.GetBytes(barcode);
            Byte[] size = { (byte)barcodeArray.Length };
            barcodeArray = join(size, barcodeArray);
            Byte[] command = join(header, barcodeArray);
            Byte[] length = { (byte)command.Length };
            command = join(length, command);
            this.sendCommand(command);
        }
        public void initialize()
        {
            Byte[] command = {2, ESC, 0x40 };
            this.sendCommand(command);
        }
        public void autoCutter()
        {
            Byte[] command = { 3, GS, 0x56, 0x00 };
            this.lineSpacing();
            this.sendCommand(command);

        }
        public void lineFeed()
        {
            Byte[] command = { 1, LF };
            this.sendCommand(command);
        }
        public void justification(char a)
        {
            Byte[] command = {3, ESC, 0x61, 0 };
            if(a == 'r'){
                command[3] = 2;
            }
            else if (a == 'c')
            {
                command[3] = 1;
            }
            else
            {
                command[3] = 0;
            }
            this.sendCommand(command);
        }

        private byte[] join(byte[] a1,byte[] a2) {
            IEnumerable<byte> rv = a1.Concat(a2);
            return rv.Cast<byte>().ToArray();
        }
    }
}
