using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DrawGraph
{
    [DataContract]
    public class Edge
    {
        [XmlAttribute("source")]
        public Node SourceNode { get; }
        [XmlAttribute("target")]
        public Node TargetNode { get; }
        [XmlAttribute("weight")]
        public int? Weight { get; set; }
        public bool IsFocused { get; }

        public Edge(Node v1, Node v2, bool IsFocused)
        {
            SourceNode = v1;
            TargetNode = v2;
            this.IsFocused = IsFocused;
        }

        public Edge(Node sourceNode, Node targetNode, int weight, bool IsFocused)
        {
            SourceNode = sourceNode;
            TargetNode = targetNode;
            Weight = weight;
            this.IsFocused = IsFocused;
        }

        public override string ToString()
        {
            if (Weight > 0)
            {
                return "Source: " + SourceNode.Name + "; Target: " + TargetNode.Name + "; Weight: " + Weight + "; Focused: " + IsFocused;
            }
            else
            {
                return "Source: " + SourceNode.Name + "; Target: " + TargetNode.Name + "; Focused: " + IsFocused;
            }
        }

        public double GetCenterByX()
        {
            return (TargetNode.X + SourceNode.X) / 2;
        }

        public double GetCenterByY()
        {
            return (TargetNode.Y + SourceNode.Y) / 2;
        }

        public static bool IsCursorOnNode(System.Windows.Point cursorPosition, Node Node)
        {
            try
            {
                if (Node.GetCenterByX() - cursorPosition.X <= Settings.NodeWidth / 2 &&
                    Node.GetCenterByX() - cursorPosition.X >= 0)
                {
                    if (Node.GetCenterByY() - cursorPosition.Y <= Settings.NodeHeight / 2 &&
                        Node.GetCenterByY() - cursorPosition.Y >= 0)
                    {
                        return true;
                    }
                }

                if (cursorPosition.X - Node.GetCenterByX() <= Settings.NodeWidth / 2 &&
                    cursorPosition.X - Node.GetCenterByX() >= 0)
                {
                    if (cursorPosition.Y - Node.GetCenterByY() <= Settings.NodeWidth / 2 &&
                        cursorPosition.Y - Node.GetCenterByY() >= 0)
                    {
                        return true;
                    }

                    return false;
                }

                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsNodeBelongToEdge(Node Node)
        {
            if (Node.Compare(Node, SourceNode) || Node.Compare(Node, TargetNode))
                return true;

            return false;
        }

        public static bool Compare(Edge edge1, Edge edge2)
        {
            if (Equals(edge1, edge2))
                return true;
            if (Node.Compare(edge1.SourceNode, edge2.SourceNode) && Node.Compare(edge1.TargetNode, edge2.TargetNode))
                return true;
            return false;
        }

        public static bool Exists(Edge[] edges, Edge edge)
        {
            foreach(var e in edges)
            {
                if (Compare(e, edge))
                    return true;
            }
            return false;
        }

        public bool Exists(Graph graph)
        {
            return Exists(graph.Edges, this);
        }
    }
}