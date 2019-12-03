using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DrawGraph
{
    public class Matrix
    {
        /// <summary>
        /// Creates an adjacency matrix from given nodes and edges
        /// </summary>
        /// <param name="nodes">Array of nodes</param>
        /// <param name="edges">Array of edges</param>
        /// <returns>Adjacency matrix in 2D int array</returns>
        public static int[,] AdjacencyCreate(Node[] nodes, Edge[] edges)
        {
            int length = nodes.Length;
            int[,] matrix = new int[length, length];
            for (int i = 0; i < nodes.Length; i++)
            {
                var Node1 = nodes[i];
                for (int j = 0; j < nodes.Length; j++)
                {
                    if (i != j)
                    {
                        var Node2 = nodes[j];
                        for (int k = 0; k < edges.Length; k++)
                        {
                            var edge = edges[k];
                            if (edge.IsFocused)
                            {
                                if (Node.Compare(edge.TargetNode, Node1) &&
                                    Node.Compare(edge.SourceNode, Node2))
                                    if (edge.Weight > 0)
                                        matrix[i, j] = (int) edge.Weight;
                                    else
                                        matrix[i, j] = 1;
                                else if (Node.Compare(edge.SourceNode, Node1) &&
                                         Node.Compare(edge.TargetNode, Node2))
                                    if (edge.Weight > 0)
                                        matrix[i, j] = (int) edge.Weight;
                            }
                            else
                            {
                                if (Node.Compare(edge.TargetNode, Node1) &&
                                    Node.Compare(edge.SourceNode, Node2))
                                    if (edge.Weight > 0)
                                    {
                                        matrix[i, j] = (int) edge.Weight;
                                        InvertElement(matrix, i, j);
                                    }
                                    else
                                    {
                                        matrix[i, j] = 1;
                                        InvertElement(matrix, i, j);
                                    }
                                else if (Node.Compare(edge.SourceNode, Node1) &&
                                         Node.Compare(edge.TargetNode, Node2))
                                    if (edge.Weight > 0)
                                    {
                                        matrix[i, j] = (int) edge.Weight;
                                        InvertElement(matrix, i, j);
                                    }
                                    else
                                    {
                                        matrix[i, j] = 1;
                                        InvertElement(matrix, i, j);
                                    }
                            }
                        }
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }

            return matrix;
        }
        /// <summary>
        /// Creates an adjacency matrix from given graph
        /// </summary>
        /// <param name="graph">Your graph</param>
        /// <returns>Adjacency matrix in 2D int array</returns>
        public static int[,] AdjacencyCreate(Graph graph)
        {
            return AdjacencyCreate(graph.Nodes, graph.Edges);
        }
        /// <summary>
        /// Creates an adjacency matrix from given nodes and edges
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        /// <param name="edges">List of edges</param>
        /// <returns>Adjacency matrix in 2D int array</returns>
        public static int[,] AdjacencyCreate(List<Node> nodes, List<Edge> edges)
        {
            Node[] node = nodes.ToArray();
            Edge[] edge = edges.ToArray();
            
            return AdjacencyCreate(node, edge);
        }
        private static int[,] InvertElement(int[,] matrix, int indexX, int indexY)
        {
            matrix[indexY, indexX] = matrix[indexX, indexY];
            return matrix;
        }

        /// <summary>
        /// Creates an incidence matrix from given nodes and edges
        /// </summary>
        /// <param name="nodes">Array of nodes</param>
        /// <param name="edges">Array of edges</param>
        /// <returns>Incidence matrix in 2D int array</returns>
        public static int[,] IncidenceCreate(Node[] nodes, Edge[] edges)
        {
            var rows = nodes.Length;
            var cols = edges.Length;
            var matrix = new int[rows, cols];
            for (var i = 0; i < rows; i++)
            {
                var Node = nodes[i];

                for (var j = 0; j < cols; j++)
                {
                    var edge = edges[j];


                    if (Node.Compare(Node, edge.TargetNode) && edge.IsFocused)
                        matrix[i, j] = -1;
                    else
                    {
                        if (Node.Compare(Node, edge.TargetNode) || Node.Compare(Node, edge.SourceNode))
                            if (edge.Weight > 0)
                                matrix[i, j] = (int)edge.Weight;
                            else
                                matrix[i, j] = 1;
                        else
                            matrix[i, j] = 0;
                    }
                }
            }

            return matrix;
        }
        /// <summary>
        /// Creates an incidence matrix from given graph
        /// </summary>
        /// <param name="graph">Your graph</param>
        /// <returns>Incidence matrix in 2D int array</returns>
        public static int[,] IncidenceCreate(Graph graph)
        {
            return IncidenceCreate(graph.Nodes, graph.Edges);
        }
        /// <summary>
        /// Creates an incidence matrix from given nodes and edges
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        /// <param name="edges">List of edges</param>
        /// <returns>Incidence matrix in 2D int array</returns>
        public static int[,] IncidenceCreate(List<Node> nodes, List<Edge> edges)
        {
            Node[] node = nodes.ToArray();
            Edge[] edge = edges.ToArray();
            return IncidenceCreate(node, edge);
        }

        /// <summary>
        /// Creates nodes and edges from adjacency matrix
        /// </summary>
        /// <param name="matrix">Adjacency matrix</param>
        /// <returns>Tuple of node and edge array</returns>
        public static Graph FromAdjacency(int[,] matrix)
        {
            Random random = new Random();
            Node[] nodes = new Node[matrix.GetLength(0)];
            Edge[] edges = new Edge[0];
            int[,] buffer = new int[edges.Length, edges.Length];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new Node(random.Next(0, Settings.CanvasWidth), random.Next(0, Settings.CanvasHeight), i.ToString());
            }

            for (int i = 0; i < nodes.Length; i++)
                for (int j = 0; j < nodes.Length; j++)
                {
                    if (matrix[i, j] > 0 && matrix[i, j] == matrix[j, i])
                    {
                        buffer[j, i] = 1;
                        buffer[i, j] = 1;
                    }
                    else if (matrix[i, j] > 0 && matrix[i, j] != matrix[j, i])
                        buffer[i, j] = 1;
                    else
                        buffer[i, j] = 0;
                }

            for (int i = 0; i < nodes.Length; i++)
            {
                var sourceNode = nodes[i];
                for (int j = 0; j < nodes.Length; j++)
                {
                    var targetNode = nodes[j];
                    if (buffer[i, j] != 1)
                    {
                        if (matrix[i, j] > 0 && matrix[j, i] == matrix[i, j])
                        {
                            Array.Resize(ref edges, edges.Length + 1);
                            var index = edges.Length - 1;
                            edges[index] = new Edge(sourceNode, targetNode, false);
                        }
                        else if (matrix[i, j] > 0 && matrix[i, j] != matrix[j, i])
                        {
                            Array.Resize(ref edges, edges.Length + 1);
                            var index = edges.Length - 1;
                            edges[index] = new Edge(sourceNode, targetNode, true);
                        }
                    }
                }
            }
            Graph graph = new Graph(nodes, edges);
            return graph;
        }
        /// <summary>
        /// Creates nodes and edges from adjacency matrix and add all elements to your canvas
        /// </summary>
        /// <param name="matrix">Adjacency matrix</param>
        /// <param name="canvas">Canvas to draw</param>
        /// <returns>Tuple of nodes and edges</returns>
        public static Graph FromAdjacencyToCanvas(int[,] matrix, Canvas canvas)
        {
            var items = FromAdjacency(matrix);
            Node[] nodes = items.Nodes;
            Edge[] edges = items.Edges;

            GraphAction.AddRangeToCanvas(nodes, edges, canvas);
            return items;
        }
        /// <summary>
        /// Creates nodes and edges from incidence matrix
        /// </summary>
        /// <param name="matrix">Incidence matrix</param>
        /// <returns>Tuple of nodes and edges array</returns>
        public static Graph FromIncidence(int[,] matrix)
        {
            Random random = new Random();
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            Node[] nodes = new Node[rows];
            Edge[] edges = new Edge[cols];
            int cost = int.MaxValue;
            Node buffer = null;

            for(int i = 0; i < rows; i++)
            {
                nodes[i] = new Node(random.Next(0, Settings.CanvasWidth), random.Next(0, Settings.CanvasHeight), i.ToString());
            }

            for(int i = 0; i < cols; i++)
            {
                for(int j = 0; j < rows; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        if (buffer == null)
                        {
                            cost = matrix[i, j];
                            buffer = nodes[j];
                        }
                        else
                        {
                            if (matrix[i, j] == (cost * (-1)))
                                edges[i] = new Edge(buffer, nodes[j], matrix[i, j], true);
                            else
                                edges[i] = new Edge(buffer, nodes[j], matrix[i, j], false);
                        }
                    }
                }
            }
            Graph graph = new Graph(nodes, edges);
            return graph;
        }
        /// <summary>
        /// Creates nodes and edges from incidence matrix and add all elements to your canvas
        /// </summary>
        /// <param name="matrix">Incidence matrix</param>
        /// <param name="canvas">Canvas to draw</param>
        /// <returns>Tuple of nodes and edges</returns>
        public static Graph FromIncidenceToCanvas(int[,] matrix, Canvas canvas)
        {
            var tuple = FromIncidence(matrix);
            Node[] nodes = tuple.Nodes;
            Edge[] edges = tuple.Edges;
            GraphAction.AddRangeToCanvas(nodes, edges, canvas);
            return tuple;
        }

        //TODO
        public static int[,] DistanceCreate(Node[] nodes, Edge[] edges)
        {
            if (edges is null)
            {
                throw new ArgumentNullException(nameof(edges));
            }

            var length = nodes.Length;
            int[,] matrix = new int[length, length];
            

            return matrix;
        }

        public static int[,] DistanceCreate(Graph graph)
        {
            return DistanceCreate(graph.Nodes, graph.Edges);
        }
    }
}
