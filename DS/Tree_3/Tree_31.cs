using System;
using System.IO;

namespace Tree_3
{
    class Tree_31
    {
        public static TreeData[] tree1;
        public static TreeData[] tree2;
        protected const int maxSize = 10;
        protected const int empty = -1;

        public int CreateTree(ref TreeData[] tree)
        {
            int ret = -1;
            int N;
            N = int.Parse(Console.ReadLine());
            if(N>0 && N<=maxSize)
            {
                bool[] checkArr = new bool[maxSize];
                tree = new TreeData[maxSize];
                for (int i = 0; i < N; i++)
                {
                    checkArr[i] = true;
                    tree[i] = new TreeData();
                }
                for (int i=0; i<N; i++)
                {
                    string[] strs = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);
                    tree[i].Context = strs[0];
                    if (strs[1] != "-")
                    {
                        tree[i].left = int.Parse(strs[1]);
                        checkArr[tree[i].left] = false;
                    }                       
                    if (strs[2] != "-")
                    {
                        tree[i].right = int.Parse(strs[2]);
                        checkArr[tree[i].right] = false;
                    }                    
                }
                for (int i = 0; i < N; i++)
                {
                    if(checkArr[i])
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
                            tree[i].Context = strs[0];
                            if (strs[1] != "-")
                            {
                                tree[i].left = int.Parse(strs[1]);
                                checkArr[tree[i].left] = false;
                            }
                            if (strs[2] != "-")
                            {
                                tree[i].right = int.Parse(strs[2]);
                                checkArr[tree[i].right] = false;
                            }
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

        public bool IsSame(int root1, int root2)
        {
            //根节点都为空
            if (root1 == empty && root2 == empty)
                return true;
            //有一个根节点为空
            if ((root1 == empty && root2 != empty) || (root1 != empty && root2 == empty))
                return false;
            //根节点内容不一样
            if (tree1[root1].Context != tree2[root2].Context)
                return false;
            //左子树都为空
            if (tree1[root1].left == empty && tree2[root2].left == empty)
                return IsSame(tree1[root1].right, tree2[root2].right);
            //左子树都不为空，且不需要互换
            if ((tree1[root1].left != empty && tree2[root2].left != empty) &&
                (tree1[tree1[root1].left].Context == tree2[tree2[root2].left].Context))
                return (IsSame(tree1[root1].left, tree2[root2].left) &&
                    IsSame(tree1[root1].right, tree2[root2].right));
            //需要互换
            else
                return (IsSame(tree1[root1].right, tree2[root2].left) &&
                    IsSame(tree1[root1].left, tree2[root2].right));
        }
    }

    class TreeData
    {
        public string Context;
        public int left = -1;
        public int right =-1;
    }
}
