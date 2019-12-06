# DrawGraph

Hey, this is the library created to help you in visualizing and creating graphs. This lib can work both in console apps and in WPF.
This library use .NET Framework 4.7.1


# Graph

To start using library you need to create graph:

> Graph graph = new Graph();

There are more than 10 overloads to easily implement graph, you can set Node and Edge arrays, graph name, direction of graph.

## Settings

This class represents the base settings of library where you can set:

 - NodeWidth - width of ellipse you will draw at canvas
 - NodeHeight - height of ellipse you will draw at canvas
 - StrokeThickness - thickness of Line element to draw edge
 - FontSize - default font size for labels at canvas
 - CanvasWidth - you need to set it by yourself for proper work of library
 - CanvasHeight - same as width
 - FillColor - color that will be used to fill ellipses and stroke lines in GetLineProperties() and GetEllipseProperties()
 - MaxDistance - minimal distance between source and target nodes of edge

## Node

Node is the base element of graph where to implement it you need only coordinates and chosen name.
Implementations of node:

> Node node = new Node(32, 54, "Node 1");

> System.Drawing.Point point = new System.Drawing.Point(32, 54);
> Node node = new Node(point, "Node 2");

Node only can store her neighbors in List. To automatically set all neighbors you need only use method
> Graph graph = new Graph(nodes, edges);
> graph.SetNeighbors();

To add node to graph you need to use method Add()
> graph.Add(node);

To remove node you need to use method Remove()
> graph.Remove(node);

To compare 2 nodes there is methods Node.Compare(node1, node2) and CompareWith(nodeToCompare)
> if (Node.Compare(node1, node2)) => return "Yes, they are equal!";

>if(node1.CompareWith(node2)) => return "Yes, they are equal!";

To check overlay of nodes there is method IsNodeOverlaid(node1, node2)
>if(IsNodeOverlaid(node1, node2)) => return "Sorry, they are overlaid";

This method isn't equal to method Compare.

 
## Edge

Edge is the connection between nodes. Edge contains information about source and target nodes, weight and direction.

To add edge you need to use method Add()
> graph.Add(edge);

To remove edge you need to use method Remove()
> graph.Remove(edge);

Also you can get center of edge to draw there some text, you just need to use methods
> GetCenterByX();
> GetCenterByY();

To check if node belong to edge use method IsNodeBelongToEdge()
> edge.IsNodeBelongToEdge(node);

It will return true or false.

To check existence of edge in graph just use method Exists
> Edge.Exists(Edge[] edges, Edge edge);
> edge.Exists(Graph graph);

To get output information about edge just use method ToString() that will return all information about edge
> Console.WriteLine(edge.ToString());

> Source: N1; Target: N2; Weight: 5; Focused: false

## Algorithm (Undone and not tested class)

To get count of steps to get from Node 1 to Node 2 there is method called DijkstraCount(graph, startIndex, finishIndex)
> int stepCount = DijkstraCount(graph, startIndex, finishIndex);

Also there are methods:
> DepthSearch(graph, startIndex);
> BreadthFirstSearch(graph, startIndex);

## Matrix

> AdjacencyCreate(Graph);

> IncidenceCreate(Graph);

> FromAdjacency(matrix);

> FromIncidence(matrix);

> FromAdjacencyToCanvas(matrix, canvas);

> FromIncidenceToCanvas(matrix, canvas);

## Export

To export graph there are couple of methods like:
> ToPng(Canvas, path);

> ToJpeg(Canvas, path);

> Print(Canvas);

> ToGraphML(Graph, path);

## Actions with graph

Class: GraphAction

> ClearCanvas(canvas); // Alias for canvas.Children.Clear();

> RemoveAllEdgesFromCanvas(canvas, lines, edges);

> RemoveLastAddedToCanvas(canvas);

> GetLineProperties(edge);

> GetEllipseProperties(node);

> AddGraphToCanvas(graph, canvas);
