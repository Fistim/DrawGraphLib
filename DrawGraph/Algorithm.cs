using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGraph
{
    public class Algorithm
    {
        private static int MinDistance(int[] path, bool[] includedToPath, int n)
        {
            // Initialize min value 
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < n; v++)
                if (includedToPath[v] == false && path[v] <= min)
                {
                    min = path[v];
                    min_index = v;
                }

            return min_index;
        }

        public static int DijkstraCount(Graph graph, int startIndex, int finishIndex)
        {
            var adj = Matrix.AdjacencyCreate(graph);
            return DijkstraCount(adj, startIndex, finishIndex);
        }

        public static int DijkstraCount(int[,] adjacencyMatrix, int startIndex, int finishIndex)
        {
            int n = adjacencyMatrix.GetLength(0);
            int[] path = new int[n];

            bool[] includedToShortestPath = new bool[n];

            for (int i = 0; i < n; i++)
            {
                path[i] = int.MaxValue;
                includedToShortestPath[i] = false;
            }

            path[startIndex] = 0;

            for (int i = 0; i < n - 1; i++)
            {
                int min = MinDistance(path, includedToShortestPath, n);

                includedToShortestPath[min] = true;

                for (int j = 0; j < n; j++)
                {
                    if (!includedToShortestPath[j] && adjacencyMatrix[min, j] != 0 &&
                        path[min] + adjacencyMatrix[min, j] < path[j])
                    {
                        path[j] = path[min] + adjacencyMatrix[min, j];
                    }
                }
            }

            return path[finishIndex];
        }

        public static Node[] DepthSearch(Graph graph, int startIndex)
        {
            bool[] visitedBool = new bool[graph.Nodes.Length];
            Node[] visited = new Node[graph.Nodes.Length];
            visitedBool[startIndex] = true;
            visited[startIndex] = new Node(graph.Nodes[startIndex].X, graph.Nodes[startIndex].Y, startIndex.ToString());
            var matrix = Matrix.AdjacencyCreate(graph.Nodes, graph.Edges);
            for (int i = 0; i < graph.Nodes.Length; i++)
            {
                if (matrix[startIndex, i] != 0 && !visitedBool[i])
                {
                    DepthSeach(graph.Nodes, graph.Edges, i);
                }
            }
            return visited;
        }

        public static Node[] DepthSeach(Node[] nodes, Edge[] edges, int startIndex)
        {
            bool[] visitedBool = new bool[nodes.Length]; 
            Node[] visited = new Node[nodes.Length];
            visitedBool[startIndex] = true;
            visited[startIndex] = new Node(nodes[startIndex].X, nodes[startIndex].Y, startIndex.ToString());
            var matrix = Matrix.AdjacencyCreate(nodes, edges);
            for(int i = 0; i < nodes.Length; i++)
            {
                if(matrix[startIndex, i]!=0 && !visitedBool[i])
                {
                    DepthSeach(nodes, edges, i);
                }
            }
            return visited;
        }
        
        public static Node[] BreadthFirstSearch(Node[] nodes, int startIndex)
        {
            Graph graph = new Graph(nodes);
            return BreadthFirstSearch(graph, startIndex);
        }

        public static Node[] BreadthFirstSearch(Graph graph, int startIndex)
        {
            var nodes = graph.Nodes;
            var startNode = nodes[startIndex];
            var visited = new HashSet<Node>();

            var queue = new Queue<Node>();
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                if (visited.Contains(vertex))
                    continue;

                visited.Add(vertex);

                foreach(var neighbor in vertex.Neighbors)
                {
                    if (!visited.Contains(neighbor))
                        queue.Enqueue(neighbor);
                }
            }
            
            return visited.ToArray();
        }
    }
}
