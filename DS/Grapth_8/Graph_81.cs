using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapth_8
{
    class Graph_81
    {
        private static RoadMap roadMap;

        public static void Begin(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string[] init = sr.ReadLine().Split(new char[] { ' '});
                    int citys = int.Parse(init[0]);
                    int roads = int.Parse(init[1]);
                    if(roads<citys-1)
                    {
                        Console.WriteLine(-1);
                        return;
                    }
                    roadMap = new RoadMap(citys);
                    for(int i=0; i<roads; i++)
                    {
                        string[] data = sr.ReadLine().Split(new char[] { ' ' });
                        int src = int.Parse(data[0]);
                        int dst = int.Parse(data[1]);
                        int cost = int.Parse(data[2]);
                        roadMap.AddRoad(src, dst, cost);
                    }
                }
            }
            roadMap.createMST();
            Console.ReadKey();
        }
    }

    class RoadMap
    {
        private int size;
        private int[][] graph;
        private int start;

        public RoadMap() { }

        public RoadMap(int size)
        {
            this.size = size;
            graph = new int[size][];
            for(int i=0; i<size; i++)
            {
                graph[i] = new int[size];
            }
        }

        public void AddRoad(int from, int to, int price)
        {
            graph[from-1][to-1] = graph[to-1][from-1] = price;
            start = from - 1;
        }

        public void createMST()
        {
            int[] dist = new int[size];
            int[] parent = new int[size];

            for (int i = 0; i < size; i++)
            {
                if (graph[start][i] > 0)
                    dist[i] = graph[start][i];
                else
                    dist[i] = int.MaxValue;
                if (i == start)
                    dist[i] = 0;
                parent[i] = -1;
            }

            int counter = 0;
            int total = 0;
            while(true)
            {
                int newStart;
                if (counter == 0)
                    newStart = start;
                else
                    newStart = findLeastCost(dist);
                if (newStart < 0)
                    break;
                total += dist[newStart];
                dist[newStart] = 0;
                counter++;
                for(int i=0; i<size; i++)
                {
                    if(graph[newStart][i]>0 && dist[i] !=0)
                    {
                        if(dist[i]>graph[newStart][i])
                        {
                            dist[i] = graph[newStart][i];
                            parent[i] = newStart;
                        }
                    }
                }
            }
            if (counter < size)
                Console.WriteLine(-1);
            else
                Console.WriteLine(total);
        }

        private int findLeastCost(int[] data)
        {
            int idx = -1;
            int minPrice = int.MaxValue;
            for(int i=0; i< data.Length; i++)
            {
                if (data[i] >0 && data[i] < minPrice)
                {
                    idx = i;
                    minPrice = data[i];
                }               
            }
            return idx;
        }
    }
}
