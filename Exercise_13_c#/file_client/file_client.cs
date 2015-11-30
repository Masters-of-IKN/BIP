using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Transportlaget;
using Library;

namespace Application
{
	public class file_client
	{
		/// <summary>
		/// The BUFSIZE.
		/// </summary>
		const int BUFSIZE = 1000;


		/// <summary>
		/// Initializes a new instance of the <see cref="file_client"/> class.
		/// 
		/// file_client metoden opretter en peer-to-peer forbindelse
		/// Sender en forspÃ¸rgsel for en bestemt fil om denne findes pÃ¥ serveren
		/// Modtager filen hvis denne findes eller en besked om at den ikke findes (jvf. protokol beskrivelse)
		/// Lukker alle streams og den modtagede fil
		/// Udskriver en fejl-meddelelse hvis ikke antal argumenter er rigtige
		/// </summary>
		/// <param name='args'>
		/// Filnavn med evtuelle sti.
		/// </param>
		public file_client(String[] args)
	    {
            var filename = args[0];
            var transportLayer = new Transport(BUFSIZE);

            try
            {
                
                if (args.Length < 1)
                {
                    Console.WriteLine("You should open the program like ./bip.exe <filename>");
                    return;
                }

                receiveFile(filename, transportLayer);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
        }

        /// <summary>
        /// Receives the file.
        /// </summary>
        /// <param name='fileName'>
        /// File name.
        /// </param>
        /// <param name='transport'>
        /// Transportlaget
        /// </param>
        private void receiveFile (String fileName, Transport transport)
		{
            // Translate the passed message into ASCII and store it as a Byte array.
            byte[] data = System.Text.Encoding.ASCII.GetBytes(fileName);

            transport.send(data, data.Length);

            // Remove path from filename.
            string file = Path.GetFileName(fileName);
            // Create BinaryWriter to write the read data to a file.
            var writer = new BinaryWriter(File.Open(file, FileMode.Create));
            int i, counter = 1;
            do
            {
                i = transport.receive(ref data);
                writer.Write(data, 0, i);

                Console.WriteLine("#{0} - {1} bytes", counter, i);

                counter++;
            } while (i > 0);

            writer.Close();
		}

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// First argument: Filname
		/// </param>
		public static void Main (string[] args)
		{
            new file_client(args);
		}
	}
}
