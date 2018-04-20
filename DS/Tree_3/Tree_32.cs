using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_3
{
    class Tree_32
    {
        public static TreeData[] Tree = new TreeData[maxSize];
        private int[] Leaves = new int[maxSize] { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1};
        private const int maxSize = 10;
        private const int empty = -1;

        public int CreateTree(ref TreeData[] tree)
        {
            int ret = -1;
            int N;
            N = int.Parse(Console.ReadLine());
            if (N > 0 && N <= maxSize)
            {
                bool[] checkArr = new bool[maxSize];
                tree = new TreeData[maxSize];
                for (int i = 0; i < N; i++)
                {
                    checkArr[i] = true;
                    tree[i] = new TreeData();
                }
                for (int i = 0; i < N; i++)
                {
                    string[] strs = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);
                    if (strs[0] != "-")
                    {
                        tree[i].left = int.Parse(strs[0]);
                        checkArr[tree[i].left] = false;
                    }
                    if (strs[1] != "-")
                    {
                        tree[i].right = int.Parse(strs[1]);
                        checkArr[tree[i].right] = false;
                    }
                    tree[i].Context = i.ToString();
                }
                for (int i = 0; i < N; i++)
                {
                    if (checkArr[i])
                    {
                        ret = i;
                        break;
                    }
                }
            }
            return ret;
        }

        public int CreateTree(ref TreeData[] tree, string path)
        {
            int ret = -1;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    int N = int.Parse(sr.ReadLine());
                    if (N > 0 && N <= maxSize)
                    {
                        bool[] checkArr = new bool[maxSize];
                        tree = new TreeData[maxSize];
                        for (int i = 0; i < N; i++)
                        {
                            checkArr[i] = true;
                            tree[i] = new TreeData();
                        }
                        for (int i = 0; i < N; i++)
                        {
                            string[] strs = sr.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);
                            if (strs[0] != "-")
                            {
                                tree[i].left = int.Parse(strs[0]);
                                checkArr[tree[i].left] = false;
                            }
                            if (strs[1] != "-")
                            {
                                tree[i].right = int.Parse(strs[1]);
                                checkArr[tree[i].right] = false;
                            }
                            tree[i].Context = i.ToString();
                        }
                        for (int i = 0; i < N; i++)
                        {
                            if (checkArr[i])
                            {
                                ret = i;
                                break;
                            }
                        }
                    }
                }
            }
            return ret;
        }

        public void findLeaves(TreeData[] tree, int root)
        {
            if (root == empty)
                return;
            Queue<TreeData> Q = new Queue<TreeData>();
            TreeData rootNode = tree[root];
            Q.Enqueue(rootNode);

            int idx = 0;
            while(Q.Count>0)
            {
                TreeData data = Q.Dequeue();
                if(data.left == empty && data.right == empty)
                {
                    int leafIdx = searchIdx(tree, data);
                    Leaves[idx++] = leafIdx;               
                }
                if(data.left != empty)
                {
                    Q.Enqueue(tree[data.left]);
                }
                if(data.right !=empty)
                {
                    Q.Enqueue(tree[data.right]);
                }
            }
            PrintOut();
        }

        private int searchIdx(TreeData[] tree, TreeData data)
        {
            int idx = 0;
            foreach(TreeData td in tree)
            {
                if(td.Context == data.Context)
                {
                    break;
                }
                idx += 1;
            }
            return idx;
        }

        private void PrintOut()
        {
            int idx = 0;
            foreach(int i in Leaves)
            {
                Console.Write(i);
                if (Leaves[idx + 1] != empty)
                {
                    Console.Write(" ");
                }
                else
                    break;
                idx += 1;
            }
        }
    }
}
