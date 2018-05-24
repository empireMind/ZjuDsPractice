using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_7
{
    class Graph_71
    {
        private static Graph_Floyd cookBook;

        public static void Initial(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string[] str = sr.ReadLine().Split(new char[] { ' ' });
                    //变形动物种类
                    int animal = int.Parse(str[0]);
                    //魔咒数量
                    int magic = int.Parse(str[1]);
                    cookBook = new Graph_Floyd(animal);
                    for(int i=0; i<magic; i++)
                    {
                        string[] detail = sr.ReadLine().Split(new char[] { ' ' });
                        int from = int.Parse(detail[0]);
                        int to = int.Parse(detail[1]);
                        int dist = int.Parse(detail[2]);
                        cookBook.addNewMagic(from, to, dist);
                    }
                }
            }
        }

        public static void Do()
        {
            cookBook.operate();
        }
    }

    class Graph_Floyd
    {
        private int size;
        private int[][] dist;
        private const int Nan = 9999999;

        /// <summary>
        /// 初始化邻接矩阵
        /// </summary>
        /// <param name="size"></param>
        public Graph_Floyd(int size)
        {
            this.size = size;
            dist = new int[size][];
            for(int i=0; i<size; i++)
            {
                dist[i] =  new int[size];
                for(int j=0; j<size; j++)
                {
                    if (i == j)
                        dist[i][j] = 0;
                    else
                        dist[i][j] = Nan;
                }
            }
        } 

        /// <summary>
        /// 在邻接矩阵中添加条目
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="length"></param>
        public void addNewMagic(int from, int to, int length)
        {
            if((from>=0 && from <=size) && (to >= 0 && to <= size))
            {
                //按1~N编号，减1
                dist[from - 1][to - 1] = length;
                //双向路径
                dist[to - 1][from - 1] = length;
            }

        }

        public void operate()
        {
            algr_Floyd();
            optimize();
        }

        /// <summary>
        /// floyd算法，计算所有节点最短距离
        /// </summary>
        private void algr_Floyd()
        {
            for(int k=0; k<size;k++)
            {
                for(int i=0; i<size; i++)
                    for(int j=0; j<size; j++)
                    {
                        if(dist[i][k] + dist[k][j] < dist[i][j])
                            dist[i][j] = dist[i][k] + dist[k][j];
                    }
            }
        }

        private void optimize()
        {
            IntWithIdx[] maxLength = new IntWithIdx[size];
            int idx = 0;
            //遍历矩阵，找出每个节点到其他节点的最大距离
            foreach (int[] row in dist)
            {
                int max = -1;
                foreach(int elem in row)
                {
                    if (elem >max)
                        max = elem;
                }
                maxLength[idx] = new IntWithIdx(max, idx++);
            }
            //在所有最大距离中找出最小值(可能不唯一)
            IntWithIdx min = new IntWithIdx(Nan, Nan);
            for (int i=0; i<size; i++)
            {
                if(maxLength[i].Compare(min, maxLength[i]) == 1)
                {
                    min = maxLength[i];
                }
            }
            if (min.value == Nan)
                Console.WriteLine(0);
            else
                Console.WriteLine((min.idx+1) + " " + min.value);
        }

        private class IntWithIdx: IComparer<IntWithIdx>
        {
            public int value { get; set; }
            public int idx { get; set; }

            public IntWithIdx(int value, int idx)
            {
                this.value = value;
                this.idx = idx;
            }

            public int Compare(IntWithIdx x, IntWithIdx y)
            {
                int ret = 0;
                if(x.value>y.value)
                {
                    ret = 1;
                }
                else if(x.value<y.value)
                {
                    ret = -1;
                }
                else if(x.value == y.value)
                {
                    //值相同时，索引号的小算小
                    if (x.idx > y.idx)
                        ret = 1;
                    else if (x.idx < y.idx)
                        ret = -1;
                    else
                        ret = 0;
                }
                return ret;
            }
        }
    }
}
