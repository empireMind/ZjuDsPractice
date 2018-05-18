using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_6
{
    class Graph_63
    {
        private static LinkGraph graph;

        public static void initialGraph(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string[] init = sr.ReadLine().Split(new char[] { ' ' });
                    int nVertex = int.Parse(init[0]);
                    int nEdge = int.Parse(init[1]);
                    graph = new LinkGraph(nVertex, nEdge);
                    for(int i = 0; i < nEdge; i++)
                    {
                        string[] data = sr.ReadLine().Split(new char[] { ' ' });
                        int from = int.Parse(data[0]);
                        int to = int.Parse(data[1]);
                        graph.addEdge(new Edge(from, to));
                    }
                    graph.initial();                   
                }
            }
        }

        public static void bfsPlus()
        {
            graph.countWithinSixLayer();
        }
    }

    class LinkGraph
    {
        private int nVertex;
        private int nEdge;
        private List<Vertex> graph = new List<Vertex>();
        private List<Edge> edge = new List<Edge>();
        private bool[] visit;

        public LinkGraph(int nVertex, int nEdge)
        {
            this.nVertex = nVertex;
            this.nEdge = nEdge;
            visit = new bool[nVertex];
            setVisitArray();
        }

        public void addEdge(Edge e)
        {
            edge.Add(e);
        }

        public void initial()
        {
            if (edge.Count == 0)
                return;
            Vertex vHead;
            for(int i=1; i<=nVertex; i++)
            {
                Vertex v = new Vertex(i);
                vHead = v;
                foreach(Edge e in edge)
                {
                    if (e.startWith(i))
                    {
                        v.AddLink(new Vertex(e.getEnd()));
                        v = v.nextNode;
                    }
                    else if (e.endWith(i))
                    {
                        v.AddLink(new Vertex(e.getHead()));
                        v = v.nextNode;
                    }
                }
                graph.Add(vHead);
            }
        }

        public void countWithinSixLayer()
        {
            int idx = 1;
            foreach(Vertex v in graph)
            {
                double count = bfsWithinSixLayer(v);               
                Console.WriteLine(idx + ": " + (count / nVertex).ToString("0.00%"));
                idx++;
            }
        }

        private double bfsWithinSixLayer(Vertex node)
        {
            //起始节点也计算在内
            double count = 1;
            setVisitArray();
            setGraphLayer();
            if (graph.Count==0 || edge.Count == 0)
                return count;
            Queue<Vertex> q = new Queue<Vertex>();
            q.Enqueue(node);
            while(q.Count>0)
            {
                Vertex v = q.Dequeue();
                Vertex vHead = v;
                visit[v.Label - 1] = true;
                while(v.nextNode != null)
                {
                    Vertex subV = graph[v.nextNode.Label-1];
                    if(visit[subV.Label -1] == false)
                    {
                        subV.Layer = vHead.Layer + 1;                       
                        q.Enqueue(subV);
                        if(subV.Layer<=6)
                            count++;
                    }                       
                    v = v.nextNode;
                }
            }
            return count;         
        }

        private void setVisitArray()
        {
            for(int i=0; i<nVertex; i++)
            {
                visit[i] = false;
            }
        }

        private void setGraphLayer()
        {
            foreach (Vertex v in graph)
            {
                v.Layer = 0;
            }
        }

    }

    class Edge
    {
        public int From;
        public int To;

        public Edge(int from, int to)
        {
            From = from;
            To = to;
        }
        public bool startWith(int i)
        {
            if (i == From)
                return true;
            else
                return false;
        }
        public bool endWith(int i)
        {
            if (i == To)
                return true;
            else
                return false;
        }
        public int getHead()
        {
            return From;
        }
        public int getEnd()
        {
            return To;
        }
    }

    class Vertex
    {
        //定点编号
        public int Label { get; set; }
        //顶点层级
        public int Layer { get; set; }
        //下一级顶点
        public Vertex nextNode; 

        public Vertex() { }
        public Vertex(int Label)
        {
            this.Label = Label;
        }
        public void AddLink(Vertex V)
        {
            nextNode = V;
        }
    }
}
