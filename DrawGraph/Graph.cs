using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DrawGraph
{
    [XmlRoot("graph")]
    public class Graph
    {
        [XmlElement("node")]
        public Node[] Nodes;
        [XmlElement("edge")]
        public Edge[] Edges;
        public string Name;
        public bool Direction;

        public Graph()
        {
            Nodes = new Node[0];
            Edges = new Edge[0];
            Name = "G";
            Direction = false;
        }

        public Graph(List<Node> nodes)
        {
            Nodes = nodes.ToArray();
            Edges = new Edge[0];
            Name = "G";
            Direction = false;
        }

        public Graph(List<Node> nodes, List<Edge> edges)
        {
            Nodes = nodes.ToArray();
            Edges = edges.ToArray();
            Name = "G";
            Direction = false;
        }

        public Graph(List<Node> nodes, List<Edge> edges, string name)
        {
            Nodes = nodes.ToArray();
            Edges = edges.ToArray();
            Name = name;
            Direction = false;
        }

        public Graph(List<Node> nodes, List<Edge> edges, bool isDirected)
        {
            Nodes = nodes.ToArray();
            Edges = edges.ToArray();
            Direction = isDirected;
            Name = "G";
        }

        public Graph(List<Node> nodes, List<Edge> edges, string name, bool isDirected)
        {
            Nodes = nodes.ToArray();
            Edges = edges.ToArray();
            Name = name;
            Direction = isDirected;
        }
             
        public Graph(Node[] nodes)
        {
            Nodes = nodes;
            Name = "G";
            Direction = false;
        }

        public Graph(Node[] nodes, Edge[] edges)
        {
            Nodes = nodes;
            Edges = edges;
            Name = "G";
            Direction = false;
        }

        public Graph(Node[] nodes, Edge[] edges, string name)
        {
            Nodes = nodes;
            Edges = edges;
            Name = name;
        }

        public Graph(Node[] nodes, Edge[] edges, bool isDirected)
        {
            Nodes = nodes;
            Edges = edges;
            Direction = isDirected;
        }

        public Graph(Node[] nodes, Edge[] edges, string name, bool isDirected)
        {
            Nodes = nodes;
            Edges = edges;
            Name = name;
            Direction = isDirected;
        }

        public void SetGraphName(string name)
        {
            Name = name;
        }

        public void Add(Node node)
        {
            var length = Nodes.Length;
            
            Array.Resize(ref Nodes, Nodes.Length + 1);
            Nodes[length] = node;
        }

        public void AddRange(Node[] node)
        {
            var startLength = Nodes.Length;
            Array.Resize(ref Nodes, Nodes.Length + node.Length);
            var j = 0;
            for(int i = startLength; i < Nodes.Length; i++)
            {
                Nodes[i] = node[j];
                j++;
            }
        }

        public void AddRange(List<Node> nodes)
        {
            var n = nodes.ToArray();
            AddRange(n);
        }

        public void Add(Edge edge)
        {
            Array.Resize(ref Edges, Edges.Length + 1);
            Edges[Edges.Length - 1] = edge;
        }
        
        public void AddRange(Edge[] edge)
        {
            var startLength = Edges.Length;
            Array.Resize(ref Edges, Edges.Length + edge.Length);
            var j = 0;
            for(int i = startLength; i < Edges.Length; i++)
            {
                Edges[i] = edge[j];
                j++;
            }
        }

        public void AddRange(List<Edge> edge)
        {
            var e = edge.ToArray();
            AddRange(e);
        }

        public void RemoveNode(int index)
        {
            Nodes = RemoveAt(Nodes, index);
        }

        public void RemoveNode(Node node)
        {
            for(int i = 0; i < Nodes.Length; i++)
            {
                if(Node.Compare(Nodes[i], node))
                {
                    Nodes = RemoveAt(Nodes, i);
                    break;
                }
            }
        }

        public void SetNeighbors()
        {
            foreach(var node in Nodes)
            {
                node.Neighbors.Clear();
                foreach(var edge in Edges)
                {
                    if (Node.Compare(node, edge.SourceNode))
                        node.Neighbors.Add(edge.TargetNode);
                    if (Node.Compare(node, edge.TargetNode))
                        node.Neighbors.Add(edge.SourceNode);
                }
            }
        }

        public Node GetNode(int index)
        {
            return Nodes[index];
        }


        public Edge GetEdge(int index)
        {
            return Edges[index];
        }

        public void RemoveEdge(int index)
        {
            Nodes = RemoveAt(Nodes, index);
        }

        public void RemoveEdge(Edge edge)
        {
            for(int i = 0; i < Edges.Length; i++)
            {
                if(Edge.Compare(Edges[i], edge))
                {
                    Edges = RemoveAt(Edges, i);
                    break;
                }
            }
        }

        private T[] RemoveAt<T>(T[] source, int index)
        {
            List<T> dest = new List<T>();
            dest.AddRange(source);
            dest.RemoveAt(index);

            return dest.ToArray();
        }

        public void Clear()
        {
            Array.Clear(Nodes, 0, Nodes.Length);
            Array.Clear(Edges, 0, Nodes.Length);
            Name = string.Empty;
            Direction = false;
        }
    }
}