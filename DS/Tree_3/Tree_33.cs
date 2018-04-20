using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_3
{
    class Tree_33
    {
        private const int maxSize = 30;
        private const int empty = -1;
        private Stack<StackTreeData> StackTree = new Stack<StackTreeData>();
        private int[] preOrder = new int[maxSize];
        private int[] inOrder = new int[maxSize];
        private Stack<int> postStack = new Stack<int>();

        public void CreatePreAndInOrder()
        {
            int N;
            N = int.Parse(Console.ReadLine());
            for (int i = 0; i < maxSize; i++)
            {
                preOrder[i] = empty;
                inOrder[i] = empty;
            }
            if (N > 0 && N <= maxSize)
            {
                int idx_pre = 0;
                int idx_in = 0;
                for (int i = 0; i < 2*N; i++)
                {
                    string[] strs = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);
                    if (strs[0] == "Push")
                    {
                        StackTreeData data = new StackTreeData();
                        data.content = int.Parse(strs[1]);
                        preOrder[idx_pre++] = data.content;
                        StackTree.Push(data);
                    }
                    else if (strs[0] == "Pop")
                    {
                        StackTreeData data = StackTree.Pop();
                        inOrder[idx_in++] = data.content;
                    }
                }
            }
            GetPostOrder(preOrder, inOrder);
            PrintOut();
        }

        public void CreatePreAndInOrder(string path)
        {
            for (int i = 0; i < maxSize; i++)
            {
                preOrder[i] = empty;
                inOrder[i] = empty;
            }
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    int N;
                    N = int.Parse(sr.ReadLine());
                    if (N > 0 && N <= maxSize)
                    {
                        int idx_pre = 0;
                        int idx_in = 0;
                        for (int i = 0; i < 2 * N; i++)
                        {
                            string[] strs = sr.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);
                            if (strs[0] == "Push")
                            {
                                StackTreeData data = new StackTreeData();
                                data.content = int.Parse(strs[1]);
                                preOrder[idx_pre++] = data.content;
                                StackTree.Push(data);
                            }
                            else if (strs[0] == "Pop")
                            {
                                StackTreeData data = StackTree.Pop();
                                inOrder[idx_in++] = data.content;
                            }
                        }
                    }
                }
            }
            GetPostOrder(preOrder, inOrder);
            PrintOut();
        }

        private void GetPostOrder(int[] preOrder, int[] inOrder)
        {
            if (GetLength(preOrder) == 0)
                return;
            int root = preOrder[0];
            postStack.Push(root);
            int[][] InOrderLR = SplitInOrder(inOrder, root);
            int[] InOrderL = InOrderLR[0];
            int[] InOrderR = InOrderLR[1];
            int[][] PreOrderLR = SplitPreOrder(preOrder, GetLength(InOrderL), GetLength(InOrderR));
            int[] PreOrderL = PreOrderLR[0];
            int[] PreOrderR = PreOrderLR[1];
            GetPostOrder(PreOrderR, InOrderR);
            GetPostOrder(PreOrderL, InOrderL);
        }

        private int GetLength(int[] arr)
        {
            int length = 0;
            foreach(int i in arr)
            {
                if (i == empty)
                    break;
                length += 1;
            }
            return length;
        }

        private int[][] SplitInOrder(int[] inOrder, int root)
        {
            int[][] lr = new int[2][];
            int idx = 0;            
            int end = GetLength(inOrder);
            foreach(int element in inOrder)
            {
                if (element == root)
                    break;
                idx += 1;
            }
            int[] left = new int[idx]; 
            for(int i = 0; i<idx; i++)
            {
                left[i] = inOrder[i];
            }
            int[] right = new int[end - idx - 1];
            for(int i=0; i<end - idx -1; i++)
            {
                right[i] = inOrder[idx + i + 1];
            }
            lr[0] = left;
            lr[1] = right;
            return lr;
        } 

        private int[][] SplitPreOrder(int[] preOrder, int leftLen, int  rightLen)
        {
            int[][] PreLR = new int[2][];
            int[] PreL = new int[leftLen];
            int[] PreR = new int[rightLen];
            for(int i=0; i<leftLen; i++)
            {
                PreL[i] = preOrder[i + 1];
            }
            for(int i=0; i<rightLen; i++)
            {
                PreR[i] = preOrder[i + 1 + leftLen];
            }
            PreLR[0] = PreL;
            PreLR[1] = PreR;
            return PreLR;
        }

        private void PrintOut()
        {
            while(postStack.Count!=0)
            {
                Console.Write(postStack.Pop());
                if (postStack.Count >= 1)
                    Console.Write(" ");
            }
        }
    }

    class StackTreeData
    {
        public int content;
        public int label_three = 0;
    }
}
