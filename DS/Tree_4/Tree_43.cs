using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_4
{
    class Tree_43
    {
        protected static int N;
        protected static int[] element;
        protected static int[] sorted;
        private static string path = "Tree43.txt";

        public static void Execute()
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    N = int.Parse(sr.ReadLine());
                    string[] str = sr.ReadLine().Split(new char[] { ' ' });
                    element = new int[N];
                    sorted = new int[N];
                    for (int i=0; i<N; i++)
                    {
                        if(i==0)
                            element[i] = int.Parse(str[i]);
                        for(int j=i+1; j<N; j++)
                        {
                            if(i==0)
                                element[j] = int.Parse(str[j]);
                            if (element[i] > element[j])
                            {
                                int temp = element[i];
                                element[i] = element[j];
                                element[j] = temp;
                            }
                        }
                    }
                }
            }
            solve(0, N-1, 0);
            PrintOut(N);
        }

        protected static void solve(int head, int end, int root)
        {
            int total = end - head + 1;
            if (total == 0)
                return;
            int Lcount = GetLeftSubTreeLength(total);
            sorted[root] = element[head + Lcount];
            solve(head, head + Lcount - 1, root * 2 + 1);
            solve(head + Lcount + 1, end, root * 2 + 2);
        }

        /// <summary>
        /// 计算左子树节点数量
        /// </summary>
        /// <returns></returns>
        protected static int GetLeftSubTreeLength(int total)
        {
            //(2^H -1) + X = N  （1）
            //H为完全二叉树出最后一层外的层数；X为完全二叉树最后一层节点数量；N为完全二叉树总结点数
            double H = Math.Floor(Math.Log(total + 1, 2));
            //X_max为左子树最后一层的最大节点数: X_max = 2^(H+1-1)/2
            double X_max = Math.Pow(2, H - 1);
            //根据式(1)计算X
            double X = total + 1 - Math.Pow(2, H);
            //如果X小于X_max,X取X_max
            X = X > X_max ? X_max : X;
            //代入式1计算新的N值，注意是2的H-1次幂       
            return (int)Math.Ceiling(Math.Pow(2, H - 1) - 1 + X);
        }

        protected static void PrintOut(int N)
        {
            for(int i=0; i<N; i++)
            {
                Console.Write(sorted[i]);
                if (i < N - 1)
                    Console.Write(" ");
            }
        }
    }
}
