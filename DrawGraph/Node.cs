using System.Xml.Serialization;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Drawing;

namespace DrawGraph
{
    public class Node
    {
        [XmlAttribute("positionX")]
        public int X { get; set; }
        [XmlAttribute("positionY")]
        public int Y { get; set; }
        public string Name { get; set; }
        public List<Node> Neighbors;

        public Node(int x, int y, string name)
        {
            X = x;
            Y = y;
            Name = name;
        }

        public Node(Point point, string name)
        {
            X = point.X;
            Y = point.Y;
            Name = name;
        }

        public override string ToString()
        {
            return "Name: " + Name + "X: " + X.ToString() + "Y: " + Y.ToString();
        }

        /// <summary>
        /// Get center of node by X-coordinate
        /// </summary>
        /// <returns>Center of node by X</returns>
        public int GetCenterByX()
        {
            return (X * 2 + (Settings.NodeWidth / 2)) / 2;
        }

        /// <summary>
        /// Get center of node by Y-coordinate
        /// </summary>
        /// <returns>Center of node by Y</returns>
        public int GetCenterByY()
        {
            return (Y * 2 + (Settings.NodeHeight / 2)) / 2;
        }

        /// <summary>
        /// Compare two nodes
        /// </summary>
        /// <param name="node1">First node to compare</param>
        /// <param name="node2">Second node to compare</param>
        /// <returns>True if nodes are equal, false if not equal</returns>
        public static bool Compare(Node node1, Node node2)
        {
            if (Equals(node1, node2))
                return true;
            if (node1 == node2)
                return true;
            if (node1.X == node2.X && node1.Y == node2.Y)
                return true;
            return false;
        }

        public bool CompareWith(Node node)
        {
            return Compare(this, node);
        }


        /// <summary>
        /// Compare nodes and returns true if they are overlaid
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns>True if overlaid, false if not</returns>
        public static bool IsNodeOverlaid(Node node1, Node node2)
        {
            var x = node1.X - node2.X;
            var y = node1.Y - node2.Y;
            if(x<=Settings.NodeWidth/2 && x>=0)
                if (y <= Settings.NodeHeight / 2 && y > 0)
                    return true;
            x = node2.X - node1.X;
            y = node2.Y - node1.Y;
            if (x <= Settings.NodeWidth / 2 && x >= 0)
                if (y <= Settings.NodeHeight / 2 && y > 0)
                    return true;
            return false;
        }


        /// <summary>
        /// Check if node has neighbors
        /// </summary>
        /// <param name="node">Node to check</param>
        /// <param name="edges">Edges array</param>
        /// <returns></returns>
        public static bool HasNeighbors(Node node, Edge[] edges)
        {
            foreach(var edge in edges)
            {
                if (Compare(node, edge.SourceNode) || Compare(node, edge.TargetNode))
                    return true;
            }
            return false;
        }
        public static bool HasNeighbors(Node node, List<Edge> edges)
        {
            var a = edges.ToArray();
            return HasNeighbors(node, a);
        }
    }
}
