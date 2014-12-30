using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace prototype.Engine.AI
{
    public class Node
    {
        // Change this depending on what the desired node size is
        public static int NODE_SIZE = 32;
        public Node Parent;
        public Vector2 Position;
        public Vector2 Center
        {
            get
            {
                return new Vector2(Position.X + NODE_SIZE / 2, Position.Y + NODE_SIZE / 2);
            }
        }
        public float DistanceToTarget;
        public float Cost;
        public float F
        {
            get
            {
                if (DistanceToTarget != -1 && Cost != -1)
                    return DistanceToTarget + Cost;
                else
                    return -1;
            }
        }
        public bool Walkable;

        public Node(Vector2 pos, bool walkable)
        {
            Parent = null;
            Position = pos;
            DistanceToTarget = -1;
            Cost = 1;
            Walkable = walkable;
        }
    }

    public class Astar
    {
        List<List<Node>> Grid;
        int GridRows
        {
            get
            {
               return Grid[0].Count;
            }
        }
        int GridCols
        {
            get
            {
                return Grid.Count;
            }
        }

        public Astar(List<List<Node>> grid)
        {
            Grid = grid;
        }


        public Stack<Node> FindPath(Vector2 Start, Vector2 End)
        {
            Node start = new Node(Start/32, true);
            Node end = new Node(End/32, true);

            Stack<Node> Path = new Stack<Node>();
            List<Node> OpenList = new List<Node>();
            List<Node> ClosedList = new List<Node>();
            List<Node> adjacencies;
            Node current = start;
           
            // add start node to Open List
            OpenList.Add(start);

            while(OpenList.Count != 0 && !ClosedList.Exists(x => x.Position == end.Position))
            {
                current = OpenList[0];
                OpenList.Remove(current);
                ClosedList.Add(current);
                adjacencies = GetAdjacentNodes(current);

 
                foreach(Node n in adjacencies)
                {
                    if (!ClosedList.Contains(n) && n.Walkable)
                    {
                        if (!OpenList.Contains(n))
                        {
                            n.Parent = current;
                            n.DistanceToTarget = Math.Abs(n.Position.X - end.Position.X) + Math.Abs(n.Position.Y - end.Position.Y);
                            n.Cost = 1 + n.Parent.Cost;
                            OpenList.Add(n);
                            OpenList = OpenList.OrderBy(node => node.F).ToList<Node>();
                        }
                        else
                        {
                            if(1 + n.Parent.Cost < current.Cost)
                            {
                                
                            }
                        }
                    }
                }
            }
            
            // construct path
            Node temp = ClosedList[ClosedList.IndexOf(current)];
            while(temp != start && temp != null)
            {
                Path.Push(temp);
                temp = temp.Parent;
            }
            //Path.Push(start);

            return Path;
        }

        private List<Node> GetAdjacentNodes(Node n)
        {
            List<Node> temp = new List<Node>();

            int row = (int)n.Position.Y;
            int col = (int)n.Position.X;

            if(row + 1 < GridRows)
            {

                //temp.Add(Grid[row + 1][col]);
                temp.Add(Grid[col][row + 1]);
            }
            if(row - 1 >= 0)
            {
                //temp.Add(Grid[row - 1][col]);
                temp.Add(Grid[col][row - 1]);
            }
            if(col - 1 >= 0)
            {
                //temp.Add(Grid[row][col - 1]);
                temp.Add(Grid[col - 1][row]);
            }
            if(col + 1 < GridCols)
            {
                //temp.Add(Grid[row][col + 1]);
                temp.Add(Grid[col + 1][row]);
            }

            return temp;
        }
    }
}

//namespace prototype.Engine.AI
//{
//    public class Graph
//    {
//        public List<Node> Nodes;
//        public List<Edge> Edges;
//        //public List<List<Edge>> Adjacencies;

//        public Graph()
//        {
//            Nodes = new List<Node>();
//            Edges = new List<Edge>();
//        }

//        public void Clear()
//        {
//            Nodes.Clear();
//            Edges.Clear();
//        }

//        public void AddNode(float x, float y)
//        {
//            AddNode(GenerateNode(x, y));
//        }

//        public void AddNode(Node newNode)
//        {
//            Nodes.Add(newNode);
//        }
        
//        public Node GenerateNode(float x, float y)
//        {
//            return new Node(x, y);
//        }

//        public void AddEdge(Edge e)
//        {
//            Edges.Add(e);
//        }

//        public void AddEdge(Node s, Node e, float w)
//        {
//            Edges.Add(GenerateEdge(s, e, w));
//        }

//        public Edge GenerateEdge(Node s, Node e, float weight)
//        {
//            return new Edge(s, e, weight);
//        }

//        public void RemoveNode(Node toRemove)
//        {
//            foreach(Edge e in toRemove.IncomingEdges)
//            {
//                e.Start.OutgoingEdges.Remove(e);
//                Edges.Remove(e);
//            }
//            foreach(Edge e in toRemove.OutgoingEdges)
//            {
//                e.End.IncomingEdges.Remove(e);
//                Edges.Remove(e);
//            }

//            Nodes.Remove(toRemove);     
//        }

//        public void RemoveEdge(Edge toRemove)
//        {
//            Edges.Remove(toRemove);
//            toRemove.Start.OutgoingEdges.Remove(toRemove);
//            toRemove.End.IncomingEdges.Remove(toRemove);
//        }

//        public Node ClosestNode(Node n1, out float Distance)
//        {
//            Node MinNode = null;
//            float DistanceMin =9999999999;

//            foreach(Node n in Nodes)
//            {
//                float TempDist = Node.Distance(n1, n);

//                if(DistanceMin > TempDist)
//                {
//                    DistanceMin = TempDist;
//                    MinNode = n;
//                }
//            }
//            Distance = DistanceMin;
//            return MinNode;
//        }
//    }


//    #region Edge
//    public class Edge
//    {
//        public Node Start;
//        public Node End;
//        public float Weight{   
//            get{ return Weight; }
//            set{ Weight = value; }     
//        }
//        public bool Passable;

//        public Edge(Node start, Node end, float weight)
//        {
//            Start = start;
//            Start.OutgoingEdges.Add(this);
//            End = end;
//            End.IncomingEdges.Add(this);
//            Weight = weight;
//        }

//    }
//    #endregion

//    #region Node
//    public class Node
//    {
//        public Node ParentNode;
//        public List<Edge> IncomingEdges;
//        public List<Edge> OutgoingEdges;
//        public bool Passable;
//        public float x 
//        {
//            get { return x; }
//            set { x = value; }
//        }
//        public float y
//        {
//            get { return y; }
//            set { x = value; }
//        }

//        public Node(float xp, float yp)
//        {
//            ParentNode = null;
//            x = xp;
//            y = yp;
//            IncomingEdges = new List<Edge>();
//            OutgoingEdges = new List<Edge>();
//            Passable = true;
//        }

//        public void ChangePosition(float xp, float yp)
//        {
//            x = xp;
//            y = yp;
//        }

//        public Node[] AccessibleNodes()
//        {
//            Node[] temp = new Node[OutgoingEdges.Count];
//            int i = 0;
//            foreach (Edge e in OutgoingEdges)
//            {
//                temp[i++] = e.End;
//            }
//            return temp;
//        }

//        public Node[] AccessingNodes()
//        {
//            Node[] temp = new Node[IncomingEdges.Count];
//            int i = 0;
//            foreach (Edge e in IncomingEdges)
//            {
//                temp[i++] = e.Start;
//            }
//            return temp;
//        }

//        public static float Distance(Node n1, Node n2)
//        {
//            if( n1 == null || n2 == null)
//            {
//                return -1;
//            }

//            float dx = n1.x - n2.x;
//            float dy = n1.y - n2.y;

//            return Math.Abs(dx) + Math.Abs(dy);
//        }
//    }
//    #endregion

//    #region Path
//    class Path
//    {
//        public static Node Target;
//        private static double c = 0.5;

//        public Node EndNode;
//        public Path Queue;
//        public int NumEdgesVisited;
//        public double Cost;

//        public bool Succeed { get { return EndNode == Target; } }

//        public Path(Node N)
//        {
//            Cost = 0;
//            NumEdgesVisited = 0;
//            Queue = null;
//            EndNode = N;
//        }

//        public Path(Path Prev, Edge Transition)
//        {
//            Queue = Prev;
//            Cost = Queue.Cost + Transition.Weight;
//            NumEdgesVisited = Queue.NumEdgesVisited + 1;
//            EndNode = Transition.End;
//        }

//        public bool SameEndNode(Path p1, Path p2)
//        {
//            return p1.EndNode == p2.EndNode;
//        }
//    }
//    #endregion

//    class Astar
//    {
//        public Graph G;
//        public List<Node> Open;
//        public List<Node> Closed;
//        public Path ToGoBack;
//        public int NumOfIterations = -1;
//        public Node Start, End;
//        public bool Initialized { get { return NumOfIterations >= 0; } }
//        public bool SearchStarted { get { return NumOfIterations > 0; } }

//        public Astar(Graph g)
//        {
//            G = g;
//            Open = new List<Node>();
//            Closed = new List<Node>();
//        }

//        public void Initialize(Node start, Node end)
//        {
//            Start = start;
//            End = end;
//            Closed.Clear();
//            Open.Clear();
//            //Path.Target = end;
//            Open.Add(new Node(start.x, start.y));
//            NumOfIterations = 0;
//            ToGoBack = null;
//        }

//        public Path FindPath()
//        {

//            // TODO: finish implementing 
//           // int IndexMin = Open
//            while(!Closed.Contains(End))
//            {
//                foreach(Node n1 in Open)
//                {
//                     foreach(Edge e in n1.OutgoingEdges)
//                     {
//                         if(e.Passable && !Closed.Contains(e.End))
//                         {
//                            // compute f
//                            double f = e.Weight + H
//                         }
//                         else
//                         {
//                             Closed.Add(e.End);
//                         }
//                     }
//                }
               
//            }

//            return ToGoBack;
//        }
//    }
//}

