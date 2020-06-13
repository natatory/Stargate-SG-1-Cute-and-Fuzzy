using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stargate_SG_1_Cute_and_Fuzzy
{
    public class PathNode
    {
        public Point Position { get; set; }
        public double PathLengthFromStart { get; set; }
        public PathNode CameFrom { get; set; }
        public double HeuristicEstimatePathLength { get; set; }
        public double EstimateFullPathLength
        {
            get
            {
                return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
            }
        }
    }
}
