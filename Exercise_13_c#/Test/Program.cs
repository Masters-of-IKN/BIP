using Linklaget;
using Transportlaget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;


namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            bool client = true;

            if (client)
            {
                var fC = new file_client(args);
            }
            else
            {
                var fS = new file_server();
            }
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();

            /*
			//Vælge om der skal sendes eller modtages
			var receive = true;

			var transport = new Transport (1000);
			var buffer = new byte[1000];


			if (receive) {
				transport.receive (ref buffer);
				string text = Encoding.ASCII.GetString (buffer, 0, buffer.Length);

				Console.WriteLine (text);
			} 
			else 
			{
				buffer = Encoding.ASCII.GetBytes ("kage");
				transport.send (buffer, buffer.Length);
			}
            */
        }
    }
}
