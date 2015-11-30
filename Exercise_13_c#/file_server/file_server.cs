using System;
using System.IO;
using System.Text;
using Transportlaget;
using Library;

namespace Application
{
	public class file_server
	{
		/// <summary>
		/// The BUFSIZE
		/// </summary>
		private const int BUFSIZE = 1000;

		/// <summary>
		/// Initializes a new instance of the <see cref="file_server"/> class.
		/// </summary>
		public file_server ()
		{
			Console.WriteLine("Waiting for request");
            var _tran = new Transport(BUFSIZE);

            Byte[] bytes = new Byte[BUFSIZE];
		    string data;

            int i = _tran.receive(ref bytes);

            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

            Console.WriteLine("Received: {0}", data);

		    if (File.Exists(data))
		    {
		        sendFile(data, data.Length, _tran);
		    }
        }

		/// <summary>
		/// Sends the file.
		/// </summary>
		/// <param name='fileName'>
		/// File name.
		/// </param>
		/// <param name='fileSize'>
		/// File size.
		/// </param>
		/// <param name='tl'>
		/// Tl.
		/// </param>
		private void sendFile(String fileName, long fileSize, Transport transport)
		{
            var reader = new BinaryReader(File.Open(fileName, FileMode.Open));

            int counter = 1;
            byte[] array;

            do
            {
                array = reader.ReadBytes(1000);
                transport.send(array, array.Length);

                Console.WriteLine("#{0} - {1} bytes", counter, array.Length);

                counter++;
            } while (array.Length > 0);

            reader.Close();
        }

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main (string[] args)
		{
			new file_server();
		}
	}
}
