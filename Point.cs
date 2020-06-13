using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stargate_SG_1_Cute_and_Fuzzy
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get => _x; private set => _x = value; }
        public int Y { get => _y; private set => _y = value; }
        private int _x;
        private int _y;
    }
}
