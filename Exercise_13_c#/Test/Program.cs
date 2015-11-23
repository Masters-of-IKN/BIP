using Linklaget;
using Transportlaget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
			var transport = new Transport(1000);
			var buffer = new byte[1000];

			transport.receive (ref buffer);
			string text = Encoding.ASCII.GetString (buffer, 0, buffer.Length);

			Console.WriteLine(text);
        }
    }
}
