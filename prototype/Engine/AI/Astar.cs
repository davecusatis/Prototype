using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prototype.Engine.AI
{
    public class Graph
    {
        public List<Node> Nodes;
        public List<Edge> Edges;

        public Graph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public void Clear()
        {
            Nodes.Clear();
            Edges.Clear();
        }

        public void AddNode(float x, float y)
        {
            AddNode(GenerateNode(x, y));
        }

        public void AddNode(Node newNode)
        {
            Nodes.Add(newNode);
        }
        
        public Node GenerateNode(float x, float y)
        {
            return new Node(x, y);
        }

        public void AddEdge(Edge e)
        {
            Edges.Add(e);
        }

        public void AddEdge(Node s, Node e, float w)
        {
            Edges.Add(GenerateEdge(s, e, w));
        }

        public Edge GenerateEdge(Node s, Node e, float weight)
        {
            return new Edge(s, e, weight);
        }

        public void RemoveNode(Node toRemove)
        {
            foreach(Edge e in toRemove.IncomingEdges)
            {
                e.Start.OutgoingEdges.Remove(e);
                Edges.Remove(e);
            }
            foreach(Edge e in toRemove.OutgoingEdges)
            {
                e.End.IncomingEdges.Remove(e);
                Edges.Remove(e);
            }

            Nodes.Remove(toRemove);     
        }

        public void RemoveEdge(Edge toRemove)
        {
            Edges.Remove(toRemove);
            toRemove.Start.OutgoingEdges.Remove(toRemove);
            toRemove.End.IncomingEdges.Remove(toRemove);
        }

        public Node ClosestNode(Node n1, out float Distance)
        {
            Node MinNode = null;
            float DistanceMin =9999999999;

            foreach(Node n in Nodes)
            {
                float TempDist = Node.Distance(n1, n);

                if(DistanceMin > TempDist)
                {
                    DistanceMin = TempDist;
                    MinNode = n;
                }
            }
            Distance = DistanceMin;
            return MinNode;
        }
    }

        
    public class Edge
    {
        public Node Start;
        public Node End;
        public float Weight{   
            get{ return Weight; }
            set{ Weight = value; }     
        }
        public bool Passable;

        public Edge(Node start, Node end, float weight)
        {
            Start = start;
            Start.OutgoingEdges.Add(this);
            End = end;
            End.IncomingEdges.Add(this);
            Weight = weight;
        }

    }

    public class Node
    {
        public List<Edge> IncomingEdges;
        public List<Edge> OutgoingEdges;
        public bool Passable;

        public float x 
        {
            get { return x; }
            set { x = value; }
        }
        public float y
        {
            get { return y; }
            set { x = value; }
        }

        public Node(float xp, float yp)
        {
            x = xp;
            y = yp;
            IncomingEdges = new List<Edge>();
            OutgoingEdges = new List<Edge>();
            Passable = true;
        }

        public void ChangePosition(float xp, float yp)
        {
            x = xp;
            y = yp;
        }

        public Node[] AccessibleNodes()
        {
            Node[] temp = new Node[OutgoingEdges.Count];
            int i = 0;
            foreach (Edge e in OutgoingEdges)
            {
                temp[i++] = e.End;
            }
            return temp;
        }

        public Node[] AccessingNodes()
        {
            Node[] temp = new Node[IncomingEdges.Count];
            int i = 0;
            foreach (Edge e in IncomingEdges)
            {
                temp[i++] = e.Start;
            }
            return temp;
        }

        public static float Distance(Node n1, Node n2)
        {
            if( n1 == null || n2 == null)
            {
                return -1;
            }

            float dx = n1.x - n2.x;
            float dy = n1.y - n2.y;

            return Math.Abs(dx) + Math.Abs(dy);
        }
    }

    class Path
    {
        public static Node Target;
        private static double c = 0.5;

        public Node EndNode;
        public Path Queue;
        public int NumEdgesVisited;
        public double Cost;

        public bool Succeed { get { return EndNode == Target; } }

        public Path(Node N)
        {
            Cost = 0;
            NumEdgesVisited = 0;
            Queue = null;
            EndNode = N;
        }

        public Path(Path Prev, Edge Transition)
        {
            Queue = Prev;
            Cost = Queue.Cost + Transition.Weight;
            NumEdgesVisited = Queue.NumEdgesVisited + 1;
            EndNode = Transition.End;
        }

        public bool SameEndNode(Path p1, Path p2)
        {
            return p1.EndNode == p2.EndNode;
        }
    }

    class Astar
    {
        public Graph G;
        public List<Path> Open;
        public List<Path> Closed;
        public Path ToGoBack;
        public int NumOfIterations = -1;

        public bool Initialized { get { return NumOfIterations >= 0; } }
        public bool SearchStarted { get { return NumOfIterations > 0; } }
        public Astar(Graph g)
        {
            G = g;
            Open = new List<Path>();
            Closed = new List<Path>();
        }

        public void Initialize(Node start, Node end)
        {
            Closed.Clear();
            Open.Clear();
            Path.Target = end;
            Open.Add(new Path(start));
            NumOfIterations = 0;
            ToGoBack = null;
        }

        public bool Next()
        {
            if (Initialized) return false;
            if (Open.Count == 0) return false;

            NumOfIterations++;

            // TODO: finish implementing 
           // int IndexMin = Open
            return false;
        }
    }
}
