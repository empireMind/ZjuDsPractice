using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_7
{
    class Graph_73
    {
        private static TourGraph map;

        public static void Initial(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string[] initial = sr.ReadLine().Split(new char[] { ' ' });
                    int citys = int.Parse(initial[0]);
                    int roads = int.Parse(initial[1]);
                    int src = int.Parse(initial[2]);
                    int dst = int.Parse(initial[3]);
                    map = new TourGraph(citys, src, dst);
                    for (int i=0; i<roads; i++)
                    {
                        string[] data = sr.ReadLine().Split(new char[] { ' ' });
                        int from = int.Parse(data[0]);
                        int to = int.Parse(data[1]);
                        int dist = int.Parse(data[2]);
                        int cost = int.Parse(data[3]);
                        map.AddItems(from, to, dist, cost);
                    }
                }
            }
        }

        public static void Do()
        {
            map.Navigate();
        }
    }

    class TourGraph
    {
        private int citys;
        private int src;
        private int dst;
        private int[][] distGraph;
        private int[][] costGraph;

        public TourGraph(int num, int src, int dst)
        {
            citys = num;
            this.src = src;
            this.dst = dst;
            distGraph = new int[citys][];
            costGraph = new int[citys][];
            for(int i=0; i<citys; i++)
            {
                distGraph[i] = new int[citys];
                costGraph[i] = new int[citys];
            }
        }

        public void AddItems(int from, int to, int dist, int cost)
        {
            distGraph[from][to] = distGraph[to][from] = dist;
            costGraph[from][to] = costGraph[to][from] = cost;
        }

        public void Navigate()
        {
            //节点src到其他节点的最短距离集合
            int[] dist = new int[citys];
            //节点在最短路径上的上一个节点的集合
            int[] path = new int[citys];
            //访问过的节点集合
            bool[] visit = new bool[citys];
            int[] cost = new int[citys];
            const int Max = 9999999;
            //初始化
            for(int i=0; i<citys; i++)
            {
                if(distGraph[src][i] > 0)
                {
                    dist[i] = distGraph[src][i];
                    cost[i] = costGraph[src][i];          
                }
                else
                {
                    dist[i] = Max;
                    cost[i] = Max;
                }
                path[i] = src;
                visit[i] = false;
            }
            dist[src] = 0;
            path[src] = -1;
            cost[src] = 0;

            //dijkstra算法，计算最短路径，存在多条最短路径时选择路费最少的          
            while(true)
            {
                //在所有未访问节点中，选择离起点最近的（返回索引）作为临时起点
                int start = FindNearest(dist, visit);
                if (start < 0)
                    break;
                visit[start] = true;
                for(int i=0; i<citys; i++)
                {
                    //如果节点i未访问，且是start的邻节点
                    if(!visit[i] && distGraph[start][i]>0)
                    {
                        //更新距离和路径，计算当前路费
                        if(distGraph[start][i] + dist[start] < dist[i])
                        {
                            dist[i] = distGraph[start][i] + dist[start];
                            cost[i] = costGraph[start][i] + cost[start];
                            path[i] = start;
                            //price[i] = GetPrice(path, i);
                        }
                        //发现另一条最短路径
                        else if (distGraph[start][i] + dist[start] == dist[i])
                        {
                            if(costGraph[start][i] + cost[start] < cost[i])
                            {
                                cost[i] = costGraph[start][i] + cost[start];
                                path[i] = start;
                            }
                        }
                    }
                }
            }           
            Console.WriteLine(dist[dst] + " " + cost[dst]);
        }

        //找到未访问顶点中，距离起点最近的一个
        private int FindNearest(int[] dist, bool[] visit)
        {
            int idx = -1;
            int nearest = int.MaxValue;
            for(int i=0; i<dist.Length; i++)
            {
                if(dist[i]<nearest && !visit[i])
                {
                    idx = i;
                    nearest = dist[i];
                }
            }
            return idx;
        }
    }
}
