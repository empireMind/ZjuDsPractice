using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapth_8
{
    class Graph_82
    {
        private static Project proj;

        public static void Begin(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string[] init = sr.ReadLine().Split(new char[] { ' ' });
                    int node = int.Parse(init[0]);
                    int edge = int.Parse(init[1]);
                    proj = new Project(node);
                    for (int i = 0; i < edge; i++)
                    {
                        string[] data = sr.ReadLine().Split(new char[] { ' ' });
                        int src = int.Parse(data[0]);
                        int dst = int.Parse(data[1]);
                        int cost = int.Parse(data[2]);
                        proj.Add(src, dst, cost);
                    }
                }
            }
            proj.test();
            Console.ReadKey();
        }
    }

    class Project
    {
        int size;
        int[][] time;
        Acitivity[] act;
        //统计节点的入度
        int[] degrees;
        bool[] visit;

        public Project(int nodes)
        {
            size = nodes;
            time = new int[size][];
            act = new Acitivity[size];
            degrees = new int[size];
            visit = new bool[size];

            for (int i=0; i<size; i++)
            {
                time[i] = new int[size];
                for(int j=0; j<size; j++)
                {
                    time[i][j] = -1;
                }
                act[i] = new Acitivity();
                visit[i] = false;
            }
        }

        public void Add(int from, int to, int cost)
        {
            time[from][to] = cost;
            degrees[to] += 1;
        }

        public void test()
        {
            List<int> collected = new List<int>();
            bool noCircle = FindStart(degrees, collected);
            if(!noCircle)
                Console.WriteLine("Impossible");
            else
            {
                Queue<int> q = new Queue<int>();
                foreach(int i in collected)
                {
                    q.Enqueue(i);                
                }
                List<int> dst = new List<int>();
                while(q.Count>0)
                {
                    int start = q.Dequeue();
                    bool hasChild = false;
                    for(int i=0; i<size; i++)
                    {
                        if (time[start][i] >= 0)
                        {
                            hasChild = true;
                            if (act[start].early + time[start][i] > act[i].early)
                                act[i].early = act[start].early + time[start][i];
                            degrees[i]--;
                            if (degrees[i] == 0)
                                q.Enqueue(i);
                        }
                    }
                    if(!hasChild && degrees[start]==0)
                    {
                        dst.Add(start);
                    }                        
                }
                if (IsSucessful(degrees))
                {
                    int earlisetTime = -1;
                    foreach(int idx in dst)
                    {
                        if (act[idx].early > earlisetTime)
                            earlisetTime = act[idx].early;
                    }
                    Console.WriteLine(earlisetTime);
                }                   
                else
                    Console.WriteLine("Impossible");
            }
        }

        private bool FindStart(int[] data, List<int> collected)
        {
            bool ret = false;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 0)
                {
                    collected.Add(i);
                    ret = true;
                }
            }
            return ret;
        }

        private bool IsSucessful(int[] data)
        {
            bool ret = true;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != 0)
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }
    }

    class Acitivity
    {
        public int early { get; set; }
        public int late { get; set; }
    }
}
