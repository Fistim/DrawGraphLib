using System;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DrawGraph
{
    public class Import
    {
        public static Graph FromGraphML(string path)
        {
            if (File.Exists(path))
            {
                Graph graph = new Graph();
                XDocument doc = XDocument.Load(path);

                foreach(var nodeElement in doc.Element("graph").Elements("node"))
                {
                    XAttribute Xattr = nodeElement.Attribute("positionX");
                    XAttribute Yattr = nodeElement.Attribute("positionY");
                    XAttribute NameAttr = nodeElement.Attribute("id");
                    Node node = new Node(Convert.ToInt32(Xattr.Value), Convert.ToInt32(Yattr.Value), NameAttr.Value);
                    graph.Add(node);
                }

                foreach(var edgeElement in doc.Element("graph").Elements("edge"))
                {
                    XAttribute Source = edgeElement.Attribute("source");
                    XAttribute Target = edgeElement.Attribute("target");
                    XAttribute Weight = edgeElement.Attribute("weight");
                    Node sourceNode, targetNode;
                    sourceNode = CheckNode(graph, Source.Value);
                    targetNode = CheckNode(graph, Target.Value);
                    Edge edge = new Edge(sourceNode, targetNode, Convert.ToInt32(Weight.Value), false);
                }
                return graph;
            }
            else
            {
                throw new FileNotFoundException();
            }

        }

        private static Node CheckNode(Graph graph, string name)
        {
            var nodes = graph.Nodes;
            foreach(var node in nodes)
            {
                if (string.Equals(name, node.Name))
                    return node;
            }
            return null;
        }

        public static void GraphMLToCanvas(string path, Canvas canvas)
        {
            
        }
    }
}