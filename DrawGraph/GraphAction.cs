using System;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DrawGraph
{
    class GraphAction
    {
        public static void ClearCanvas(Canvas canvas)
        {
            canvas.Children.Clear();
        }

        public static void RemoveAllEdgesFromCanvas(Canvas canvas, Line[] lines, Edge[] edges)
        {
            Array.Clear(edges, 0, edges.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                canvas.Children.Remove(lines[i]);
            }
        }

        public static void RemoveLastAddedFromCanvas(Canvas canvas)
        {
            int removingElement = canvas.Children.Count - 1;
            canvas.Children.RemoveAt(removingElement);
        }

        public Line GetLineProperties(Edge edge)
        {
            Line line = new Line
            {
                X1 = edge.SourceNode.X,
                Y1 = edge.SourceNode.Y,
                X2 = edge.TargetNode.X,
                Y2 = edge.TargetNode.Y,
                Stroke = Settings.FillColor,
                StrokeThickness = Settings.StrokeThickness
            };
            return line;
        }

        public Ellipse GetEllipseProperties(Node node)
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = Settings.FillColor,
                Width = Settings.NodeWidth,
                Height = Settings.NodeHeight
            };
            Canvas.SetLeft(ellipse, node.X);
            Canvas.SetTop(ellipse, node.Y);
            return ellipse;
        }

        public static void AddGraphToCanvas(Graph graph, Canvas canvas)
        {
            AddRangeToCanvas(graph.Nodes, graph.Edges, canvas);
        }

        public static void AddRangeToCanvas(Node[] nodes, Edge[] edges, Canvas canvas)
        {
            Ellipse[] ellipses = new Ellipse[nodes.Length];
            Line[] lines = new Line[edges.Length];
            ArrowLine[] arrowLines = new ArrowLine[edges.Length];

            for (int i = 0; i < nodes.Length; i++)
            {
                ellipses[i] = new Ellipse()
                {
                    Fill = Settings.FillColor,
                    Width = Settings.NodeWidth,
                    Height = Settings.NodeHeight
                };
                Canvas.SetLeft(ellipses[i], nodes[i].X);
                Canvas.SetTop(ellipses[i], nodes[i].Y);
                canvas.Children.Add(ellipses[i]);
            }

            for (int i = 0; i < edges.Length; i++)
            {
                var edge = edges[i];
                if (edge.IsFocused)
                {
                    arrowLines[i] = new ArrowLine()
                    {
                        X1 = edge.SourceNode.GetCenterByX(),
                        Y1 = edge.SourceNode.GetCenterByY(),
                        X2 = edge.TargetNode.GetCenterByX(),
                        Y2 = edge.TargetNode.GetCenterByY(),
                        Stroke = Settings.FillColor,
                        Fill = Settings.FillColor,
                        StrokeThickness = Settings.StrokeThickness
                    };
                    canvas.Children.Add(arrowLines[i]);
                }
                else
                {
                    lines[i] = new Line()
                    {
                        X1 = edge.SourceNode.GetCenterByX(),
                        Y1 = edge.SourceNode.GetCenterByY(),
                        X2 = edge.TargetNode.GetCenterByX(),
                        Y2 = edge.TargetNode.GetCenterByY(),
                        Stroke = Settings.FillColor,
                        Fill = Settings.FillColor,
                        StrokeThickness = Settings.StrokeThickness
                    };
                    canvas.Children.Add(lines[i]);
                }
            }
        }
    }
}
