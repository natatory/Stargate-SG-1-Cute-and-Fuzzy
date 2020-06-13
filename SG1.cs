using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stargate_SG_1_Cute_and_Fuzzy
{

    public class SG1
    {
        public static string WireDHD(string existingWires)
        {
            int fieldWidth = existingWires.TakeWhile(c => c != '\n').Count();
            int fieldHight = existingWires.Count(c => c == '\n') + 1;
            char[,] field = new char[fieldHight, fieldWidth];
            Point startPoint = null;
            Point goalPoint = null;
            int k;
            for (int i = 0; i < fieldHight; i++)
            {
                k = 0;
                foreach (char c in existingWires.TakeWhile(c => c != '\n'))
                {
                    field[i, k] = c;
                    if (c == 'S') startPoint = new Point(i, k);
                    else if (c == 'G') goalPoint = new Point(i, k);
                    k++;
                }
                if (existingWires.Contains('\n')) existingWires = existingWires.Remove(0, fieldWidth + 1);
            }
            field = FindPath(field, startPoint, goalPoint);
            if (field == null) return "Oh for crying out loud...";
            StringBuilder outStr = new StringBuilder();
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (k = 0; k < field.GetLength(1); k++)
                {
                    outStr.Append(field[i, k]);
                }
                if (field.GetLength(0) - i != 1) outStr.AppendLine();
            }
            return outStr.ToString();
        }

        private static char[,] FindPath(char[,] field, Point start, Point goal)
        {
            var closedSet = new HashSet<PathNode>();
            var openSet = new HashSet<PathNode>();
            PathNode startNode = new PathNode()
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal)
            };
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                var currentNode = openSet.OrderBy(node =>
                  node.EstimateFullPathLength).First();
                if (currentNode.Position.X == goal.X && currentNode.Position.Y == goal.Y)
                    return GetResultField(field, currentNode);
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                foreach (var neighbourNode in GetNeighbours(currentNode, goal, field))
                {
                    if (closedSet.Count(node => node.Position.X == neighbourNode.Position.X
                        && node.Position.Y == neighbourNode.Position.Y) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node =>
                      node.Position.X == neighbourNode.Position.X
                      && node.Position.Y == neighbourNode.Position.Y);
                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                        if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
            return null;
        }

        private static double GetHeuristicPathLength(Point from, Point to)
        {
            return Math.Sqrt(Math.Pow((Math.Abs(from.X - to.X)), 2)
                + Math.Pow(Math.Abs(from.Y - to.Y), 2));
        }

        private static HashSet<PathNode> GetNeighbours(PathNode pathNode,
            Point goal, char[,] field)
        {
            var result = new HashSet<PathNode>();

            Point[] neighbourPoints = new Point[8];
            neighbourPoints[0] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
            neighbourPoints[1] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);
            neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
            neighbourPoints[3] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
            neighbourPoints[4] = new Point(pathNode.Position.X + 1, pathNode.Position.Y + 1);
            neighbourPoints[5] = new Point(pathNode.Position.X + 1, pathNode.Position.Y - 1);
            neighbourPoints[6] = new Point(pathNode.Position.X - 1, pathNode.Position.Y - 1);
            neighbourPoints[7] = new Point(pathNode.Position.X - 1, pathNode.Position.Y + 1);

            foreach (var point in neighbourPoints)
            {
                if (point.X < 0 || point.X >= field.GetLength(0))
                    continue;
                if (point.Y < 0 || point.Y >= field.GetLength(1))
                    continue;
                if (field[point.X, point.Y] == 'X')
                    continue;
                var neighbourNode = new PathNode()
                {
                    Position = point,
                    CameFrom = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart
                    + GetDistanceForNeighbourPoint(point, pathNode),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
                };
                result.Add(neighbourNode);
            }
            return result;
        }
        private static double GetDistanceForNeighbourPoint(Point point, PathNode pathNode)
        {
            if (point.X == pathNode.Position.X
                || point.Y == pathNode.Position.Y)
                return 1;
            else return Math.Sqrt(2);
        }
        private static char[,] GetResultField(char[,] field, PathNode pathNode)
        {
            var result = new HashSet<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                if (field[currentNode.Position.X, currentNode.Position.Y] != 'S' &&
                   field[currentNode.Position.X, currentNode.Position.Y] != 'G')
                    field[currentNode.Position.X, currentNode.Position.Y] = 'P';
                currentNode = currentNode.CameFrom;
            }

            return field;
        }
    }
}

