using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.Windows.Media.Imaging;
using System.Text;
using System.Web.Script.Serialization;

namespace DrawGraph
{
    public class Export
    {
        public static void ToPng(Canvas canvas, string path)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)canvas.Width, (int)canvas.Height,
                96d, 96d, PixelFormats.Pbgra32);
            canvas.Measure(new Size((int)canvas.Width, (int)canvas.Height));
            canvas.Arrange(new Rect(new Size((int)canvas.Width, (int)canvas.Height)));

            renderBitmap.Render(canvas);

            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                encoder.Save(fs);
            }
        }

        public static void ToJpeg(Canvas canvas, string path)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)canvas.Width, (int)canvas.Height,
                96d, 96d, PixelFormats.Pbgra32);
            canvas.Measure(new Size((int)canvas.Width, (int)canvas.Height));
            canvas.Arrange(new Rect(new Size((int)canvas.Width, (int)canvas.Height)));

            renderBitmap.Render(canvas);

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                encoder.Save(fs);
            }
        }

        public static void Print(Canvas canvas)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                pd.PrintVisual(canvas, "Printed with DrawGraph");
            }
        }

        public static void ToGraphML(Node[] nodes, bool direction, string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = doc.CreateElement("graph");
            var attr = doc.CreateAttribute("id");
            attr.InnerText = "G";
            root.Attributes.Append(attr);
            var dir = doc.CreateAttribute("edgedefault");
            dir.InnerText = direction ? "directed" : "undirected";
            root.Attributes.Append(dir);
            doc.AppendChild(declaration);
            doc.AppendChild(root);
            XmlProcessingInstruction pi =
                doc.CreateProcessingInstruction("graphml", "xmlns=\"http://graphml.graphdrawing.org/xmlns \" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance \" xsi: schemaLocation = \"http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd \"");
            doc.InsertBefore(pi, doc.ChildNodes[1]);

            for (int i = 0; i < nodes.Length; i++)
            {
                XmlNode Node = doc.CreateElement("node");
                var attribute = doc.CreateAttribute("id");
                attribute.InnerText = "n" + i.ToString();
                Node.Attributes.Append(attribute);
                root.AppendChild(Node);
            }
            doc.Save(path);
        }

        public static void ToGraphML(Node[] nodes, Edge[] edges, bool direction, string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = doc.CreateElement("graph");
            var attr = doc.CreateAttribute("id");
            attr.InnerText = "G";
            root.Attributes.Append(attr);
            var dir = doc.CreateAttribute("edgedefault");
            dir.InnerText = direction ? "directed" : "undirected";
            root.Attributes.Append(dir);
            doc.AppendChild(declaration);
            doc.AppendChild(root);
            XmlProcessingInstruction pi =
                doc.CreateProcessingInstruction("graphml", "xmlns=\"http://graphml.graphdrawing.org/xmlns \" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance \" xsi: schemaLocation = \"http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd \"");
            doc.InsertBefore(pi, doc.ChildNodes[1]);

            for (int i = 0; i < nodes.Length; i++)
            {
                XmlNode node = doc.CreateElement("node");
                var attribute = doc.CreateAttribute("id");
                attribute.InnerText = "n" + i.ToString();
                node.Attributes.Append(attribute);
                var XCoord = doc.CreateAttribute("positionX");
                XCoord.InnerText = nodes[i].X.ToString();
                node.Attributes.Append(XCoord);
                var YCoord = doc.CreateAttribute("positionY");
                YCoord.InnerText = nodes[i].Y.ToString();
                node.Attributes.Append(YCoord);
                root.AppendChild(node);
            }

            for (int i = 0; i < edges.Length; i++)
            {
                XmlNode edge = doc.CreateElement("edge");
                var sourceAttribute = doc.CreateAttribute("source");
                for (int j = 0; j < nodes.Length; j++)
                    if (Node.Compare(edges[i].SourceNode, nodes[j]))
                        sourceAttribute.InnerText = "n" + j.ToString();
                edge.Attributes.Append(sourceAttribute);
                var targetAttribute = doc.CreateAttribute("target");
                for (int j = 0; j < nodes.Length; j++)
                    if (Node.Compare(edges[i].TargetNode, nodes[j]))
                        targetAttribute.InnerText = "n" + j.ToString();
                edge.Attributes.Append(targetAttribute);
                if (edges[i].Weight > 0)
                {
                    var weightAttr = doc.CreateAttribute("weight");
                    weightAttr.InnerText = edges[i].Weight.ToString();
                    edge.Attributes.Append(weightAttr);
                }
                root.AppendChild(edge);
            }
            doc.Save(path);
        }

        public static void ToGraphML(Node[] nodes, Edge[] edges, string name, bool direction, string path)
        {
            Graph graph = new Graph(nodes, edges, name, direction);
            ToGraphML(graph, path);
        }

        public static void ToGraphML(Graph graph, string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = doc.CreateElement("graph");
            var attr = doc.CreateAttribute("id");
            attr.InnerText = graph.Name;
            root.Attributes.Append(attr);
            var dir = doc.CreateAttribute("edgedefault");
            dir.InnerText = graph.Direction ? "directed" : "undirected";
            root.Attributes.Append(dir);
            doc.AppendChild(declaration);
            doc.AppendChild(root);
            XmlProcessingInstruction pi =
                doc.CreateProcessingInstruction("graphml", "xmlns=\"http://graphml.graphdrawing.org/xmlns \" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance \" xsi: schemaLocation = \"http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd \"");
            doc.InsertBefore(pi, doc.ChildNodes[1]);

            for (int i = 0; i < graph.Nodes.Length; i++)
            {
                XmlNode node = doc.CreateElement("node");
                var attribute = doc.CreateAttribute("id");
                attribute.InnerText = "n" + i.ToString();
                node.Attributes.Append(attribute);
                var XCoord = doc.CreateAttribute("positionX");
                XCoord.InnerText = graph.Nodes[i].X.ToString();
                node.Attributes.Append(XCoord);
                var YCoord = doc.CreateAttribute("positionY");
                YCoord.InnerText = graph.Nodes[i].Y.ToString();
                node.Attributes.Append(YCoord);
                root.AppendChild(node);
            }

            for (int i = 0; i < graph.Edges.Length; i++)
            {
                XmlNode edge = doc.CreateElement("edge");
                var sourceAttribute = doc.CreateAttribute("source");
                for (int j = 0; j < graph.Nodes.Length; j++)
                    if (Node.Compare(graph.Edges[i].SourceNode, graph.Nodes[j]))
                        sourceAttribute.InnerText = "n" + j.ToString();
                edge.Attributes.Append(sourceAttribute);
                var targetAttribute = doc.CreateAttribute("target");
                for (int j = 0; j < graph.Nodes.Length; j++)
                    if (Node.Compare(graph.Edges[i].TargetNode, graph.Nodes[j]))
                        targetAttribute.InnerText = "n" + j.ToString();
                edge.Attributes.Append(targetAttribute);
                if (graph.Edges[i].Weight > 0)
                {
                    var weightAttr = doc.CreateAttribute("weight");
                    weightAttr.InnerText = graph.Edges[i].Weight.ToString();
                    edge.Attributes.Append(weightAttr);
                }
                root.AppendChild(edge);
            }
            doc.Save(path);
        }
    }
}