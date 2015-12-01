using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

/// <summary>
/// Link.
/// </summary>
namespace Linklaget
{
    /// <summary>
    /// Link.
    /// </summary>
    public class Link
    {
        /// <summary>
        /// The DELIMITE for slip protocol.
        /// </summary>
        const byte DELIMITER = (byte)'A';
        /// <summary>
        /// The buffer for link.
        /// </summary>
        private byte[] buffer;
        /// <summary>
        /// The serial port.
        /// </summary>
		SerialPort serialPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="link"/> class.
        /// </summary>
        public Link(int BUFSIZE)
        {
            // Create a new SerialPort object with default settings.
            serialPort = new SerialPort("/dev/ttyS1", 115200, Parity.None, 8, StopBits.One);

            if (!serialPort.IsOpen)
                serialPort.Open();

            buffer = new byte[(BUFSIZE * 2) + 2];
        }

        /// <summary>
        /// Send the specified buf and size.
        /// </summary>
        /// <param name='buf'>
        /// Buffer.
        /// </param>
        /// <param name='size'>
        /// Size.
        /// </param>
        public void send(byte[] buf, int size)
        {
			Console.WriteLine("Link sending: {0}", Encoding.ASCII.GetString(buf, 0, size));
            buffer[0] = (byte)'A';
            int pos = encode(buf, buffer, size);
            buffer[pos] = (byte)'A';

            serialPort.Write(buffer, 0, size + 2);
        }

        /// <summary>
        /// Receive the specified buf and size.
        /// </summary>
        /// <param name='buf'>
        /// Buffer.
        /// </param>
        /// <param name='size'>
        /// Size.
        /// </param>
        public int receive(ref byte[] buf)
        {
            byte c;
            int i = 0;

            do
            {
				c = (byte)serialPort.ReadByte();
                buffer[i++] = c;

            } while (c != 'A');

            do
            {
                c = (byte)serialPort.ReadByte();
                buffer[i++] = c;
            } while (c != 'A');

            int length = decode(ref buf, i);

			return length;
        }

        private int encode(byte[] data, byte[] buffer, int size)
        {
            int pos = 1;

            foreach (byte ch in data)
            {
                if (ch == 'A')
                {
                    buffer[pos++] = (byte)'B';
                    buffer[pos++] = (byte)'C';
                }
                else if (ch == 'B')
                {
                    buffer[pos++] = (byte)'B';
                    buffer[pos++] = (byte)'D';
                }
                else
                {
                    buffer[pos++] = ch;
                }
            }

            return pos;
        }

        private int decode(ref byte[] output, int size)
        {
            List<byte> list = new List<byte>();

            for (int i = 0; i < size; i++)
            {
                if (buffer[i] == 'A') continue;

                if (buffer[i] == 'B' && buffer[i + 1] == 'C')
                {
                    list.Add((byte)'A');
                    i++;
                }
                else if (buffer[i] == 'B' && buffer[i + 1] == 'D')
                {
                    list.Add((byte)'B');
                    i++;
                }
                else
                {
                    list.Add(buffer[i]);
                }
            }

            output = list.ToArray();

            return output.Length;
        }
    }
}
