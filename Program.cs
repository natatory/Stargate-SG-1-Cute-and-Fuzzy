using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stargate_SG_1_Cute_and_Fuzzy
{
    class Program
    {
        static void Main(string[] args)
        {
            var existingWires = "XX.S.XXX..\n" +
                            "XXXX.X..XX\n" +
                            "...X.XX...\n" +
                            "XX...XXX.X\n" +
                            "....XXX...\n" +
                            "XXXX...XXX\n" +
                            "X...XX...X\n" +
                            "X...X...XX\n" +
                            "XXXXXXXX.X\n" +
                            "G........X";
            string result = SG1.WireDHD(existingWires);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
