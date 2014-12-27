using System;
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
            End = end;
            Weight = weight;
        }

    }

    public class Node
    {
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
        }
    }

    class Astar
    {
    }
}
