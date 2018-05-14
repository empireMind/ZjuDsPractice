using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_5
{
    class Tree_51
    {
        protected static int N;
        protected static int M;
        protected static int[] element;
        protected static int[] index;
        protected static string path = "Tree51.txt";

        public static void Init()
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string str = sr.ReadLine();
                    string[] MN = str.Split(new char[] { ' ' });
                    M = int.Parse(MN[0]);
                    N = int.Parse(MN[1]);

                    str = sr.ReadLine();
                    string[] elem_str = str.Split(new char[] { ' ' });
                    element = new int[M+1];
                    for (int i=0; i<=M; i++)
                    {
                        if (i == 0)
                            element[i] = -99999;
                        else
                            element[i] = int.Parse(elem_str[i-1]);
                    }

                    str = sr.ReadLine();
                    string[] idx_str = str.Split(new char[] { ' '});
                    index = new int[N];
                    for (int i=0; i<N; i++)
                    {
                        index[i] = int.Parse(idx_str[i]);
                    }
                }
            }
        }

        public static void CreateMinHeap()
        {
            int idx = M;
            for(; idx>=0; idx--)
            {
                if(element[idx]<element[idx/2])
                {
                    swap(idx, idx / 2);
                }
            }
        }

        private static void swap(int idx, int idx_root)
        {
            int temp = element[idx];
            element[idx] = element[idx_root];
            element[idx_root] = temp;
            int leftson = idx * 2;
            int rightson = leftson + 1;
            if (rightson <= M)
            {
                if (element[rightson] < element[leftson])
                {
                    if (element[rightson] < element[idx])
                        swap(rightson, idx);
                }
                else
                {
                    if (element[leftson] < element[idx])
                        swap(leftson, idx);
                }
            }
            else if (leftson <= M)
            {
                if (element[leftson] < element[idx])
                    swap(leftson, idx);
            }
        }

        public static void Print()
        {
            for(int i = 0; i<N; i++)
            {
                PrintTrack(index[i]);
                if (i != N - 1)
                    Console.Write("\n");
            }
        }

        private static void PrintTrack(int i)
        {
            int[] array = new int[M];
            int idx = 0;
            while(i != 0)
            {
                array[idx++] = element[i];
                i = i / 2;
            }
            for(int j=0; j<idx; j++)
            {
                Console.Write(array[j]);
                if (j != idx - 1)
                    Console.Write(" ");
            }
        }
    }
}
