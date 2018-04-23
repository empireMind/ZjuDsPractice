using System;
using System.IO;

namespace Tree_4
{
    class Tree_42
    {
        private static string path = "Tree42.txt";
        protected static int N;
        protected static int[] element;
        protected static AVLTree root;

        public static void Execute()
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    N = int.Parse(sr.ReadLine());
                    element = new int[N];
                    string[] str = sr.ReadLine().Split(new char[] { ' ' });
                    for(int i=0; i<N; i++)
                    {
                        element[i] = int.Parse(str[i]);
                    }
                    CteateAVLTree(element, N);
                }
            }
        }

        protected static void CteateAVLTree(int[] element, int len)
        {
            for (int i = 0; i < N; i++)
            {
                if(i==0)
                    root = new AVLTree(element[0]);
                else
                {
                    AVLTree node = new AVLTree(element[i]);
                    Insert(ref root, node);
                }
            }
            Console.Write(root.data);
        }

        protected static void Insert(ref AVLTree root, AVLTree node)
        {
            if (root == null)
                root = node;
            else
            {
                if (node.data < root.data)
                    Insert(ref root.Left, node);
                else if (node.data > root.data)
                    Insert(ref root.Right, node);
            }
            root.diff = GetHeight(ref root.Left) - GetHeight(ref root.Right);
            if(root.diff>1)
            {
                if (root.Left.data < node.data)
                    root = LeftRightRotation(root);
                else 
                    root = SingleLeftRotation(root);
            }
            else if(root.diff<-1)
            {
                if (root.Right.data > node.data)
                    root = RightLeftRotation(root);
                else
                    root = SingleRightRotation(root);
            }
        }

        protected static AVLTree SingleLeftRotation(AVLTree node)
        {
            AVLTree leftNode = node.Left;
            node.Left = leftNode.Right;
            leftNode.Right = node;
            leftNode.diff = GetHeight(ref leftNode.Left) - GetHeight(ref leftNode.Right);
            node.diff = GetHeight(ref node.Left) - GetHeight(ref node.Right);
            return leftNode;
        }

        protected static AVLTree SingleRightRotation(AVLTree node)
        {
            AVLTree rightNode = node.Right;
            node.Right = rightNode.Left;
            rightNode.Left = node;
            rightNode.diff = GetHeight(ref rightNode.Left) - GetHeight(ref rightNode.Right);
            node.diff = GetHeight(ref node.Left) - GetHeight(ref node.Right);
            return rightNode;
        }

        protected static AVLTree LeftRightRotation(AVLTree node)
        {
            node.Left = SingleRightRotation(node.Left);
            return SingleLeftRotation(node);
        }

        protected static AVLTree RightLeftRotation(AVLTree node)
        {
            node.Right = SingleLeftRotation(node.Right);
            return SingleRightRotation(node);
        }

        protected static int GetHeight(ref AVLTree root)
        {
            int leftHeight = 0, rightHeight = 0;
            if (root == null)
                return 0;
            if (root.Left != null)
                leftHeight = GetHeight(ref root.Left) + 1;
            else
                leftHeight = 1;
            if (root.Right != null)
                rightHeight = GetHeight(ref root.Right) + 1;
            else
                rightHeight = 1;
            return leftHeight > rightHeight ? leftHeight : rightHeight;
        }
    }

    class Tree_42_Console : Tree_42
    {
        public static new void Execute()
        {
            N = int.Parse(Console.ReadLine());
            element = new int[N];
            string[] str = Console.ReadLine().Split(new char[] { ' ' });
            for (int i = 0; i < N; i++)
            {
                element[i] = int.Parse(str[i]);
            }
            CteateAVLTree(element, N);
        }
    }

    class AVLTree
    {
        public int data;
        public AVLTree Left = null;
        public AVLTree Right = null;
        public int diff = 0;

        public AVLTree() { }

        public AVLTree(int data)
        {
            this.data = data;
        } 
    }
}
