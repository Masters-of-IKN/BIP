using Linklaget;
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
            var link = new Link(1000);

			var ByteArray = Encoding.ASCII.GetBytes ("FALLOUTERAWESOME");

			link.send (ByteArray, ByteArray.Length);
        }
    }
}
