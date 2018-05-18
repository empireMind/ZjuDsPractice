using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_6
{
    class Graph_61
    {
        private static Graph graph;

        public static void createGraphfromFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using(StreamReader sr = new StreamReader(fs))
                {
                    string[] data = sr.ReadLine().Split(new char[] { ' ' });
                    int vertexCnt = int.Parse(data[0]);
                    int edgeCnt = int.Parse(data[1]);
                    graph = new Graph(vertexCnt);
                    for (int i=0; i<edgeCnt; i++)
                    {
                        string[] edge = sr.ReadLine().Split(new char[] { ' ' });
                        graph.drawEdge(int.Parse(edge[0]), int.Parse(edge[1]));
                    }
                }
            }
        }

        public static void DFS()
        {
            if (graph != null)
                graph.searchFunc("DFS");
        }

        public static void BFS()
        {
            if (graph != null)
                graph.searchFunc("BFS");
        }
    }

    class Graph
    {
        private int[][] graph;
        private bool[] visitList;

        public Graph(int size)
        {
            graph = new int[size][];
            visitList = new bool[size];
            for(int i=0; i<size; i++)
            {
                graph[i] = new int[size];
                visitList[i] = false;
            }            
        }

        public void drawEdge(int v, int w)
        {
            graph[v][w] = 1;
            graph[w][v] = 1;
        }

        private void DFS(int start, List<int> list)
        {
            int[] row = graph[start];
            if (visitList[start] == false)
            {
                list.Add(start);
                visitList[start] = true;
            }
            int idx = 0;
            foreach (int elem in row)
            {
                if(elem == 1 && visitList[idx] == false)
                {
                    list.Add(idx);
                    visitList[idx] = true;
                    DFS(idx, list);
                }
                idx += 1;
            }
        }
      
        private void BFS(int start, List<int> list)
        {
            int[] row = graph[start];
            Queue<int> q = new Queue<int>();
            if(visitList[start] == false)
            {
                list.Add(start);
                visitList[start] = true;
                q.Enqueue(start);
            }
            while(q.Count>0)
            {
                int pop = q.Dequeue();
                int[] pop_Row = graph[pop];
                for(int i=0; i< pop_Row.Length; i++)
                {
                    if(pop_Row[i]==1 && visitList[i] == false)
                    {
                        q.Enqueue(i);
                        list.Add(i);
                        visitList[i] = true;
                    }
                }
            }
        }

        public void searchFunc(string method)
        {
            List<int> route = new List<int>();
            for (int i = 0; i < visitList.Length; i++)
            {
                visitList[i] = false;
            }
            for (int i = 0; i < visitList.Length; i++)
            {
                if (method == "DFS")
                    DFS(i, route);
                else if (method == "BFS")
                    BFS(i, route);
                else
                    break;
                if (route.Count != 0)
                {
                    print(route);
                    route.Clear();
                }
            }
        }

        private void print(List<int> list)
        {
            Console.Write("{");
            foreach(int i in list)
            {              
                Console.Write(" " + i);              
            }
            Console.Write(" }\n");
        }
    }
}
