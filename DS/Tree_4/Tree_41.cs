using System;
using System.IO;

namespace Tree_4
{
    class Tree_41
    {
        private static string path = "Tree41.txt";
        protected static int len = 0;
        protected static int N = 0;

        public static void Execute()
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while(true)
                    {
                        string init = sr.ReadLine();
                        string[] lenN = init.Split(new char[] { ' ' });
                        len = int.Parse(lenN[0]);
                        if (len == 0)
                            break;
                        N = int.Parse(lenN[1]);
                        TreeNode root = CreateTree(sr, len);
                        for(int i=0; i<N; i++)
                        {
                            string str = sr.ReadLine();
                            string[] element = str.Split(new char[] { ' ' });
                            bool isSame = true;
                            foreach(string str_elem in element)
                            {
                                int value = int.Parse(str_elem);
                                if(isSame)
                                    isSame = Traversal(ref root, value);
                            }
                            if (isSame)
                                Console.WriteLine("Yes");
                            else
                                Console.WriteLine("No");
                            Reset(ref root);
                        }
                    }
                }
            }
        }
        
        private static TreeNode CreateTree(StreamReader sr, int len)
        {
            if (len < 1)
                return null;
            string init = sr.ReadLine();
            string[] element = init.Split(new char[] { ' ' });
            TreeNode root = new TreeNode();
            for(int i=0; i<len; i++)
            {
                if(i==0)
                    root.data = int.Parse(element[0]);
                else
                {
                    TreeNode node = new TreeNode(int.Parse(element[i]));
                    if (node.data > root.data)
                        Insert(ref root.Right, node);
                    else if (node.data < root.data)
                        Insert(ref root.Left, node);
                }
            }
            return root;
        }

        protected static void Insert(ref TreeNode root, TreeNode newNode)
        {
            if(root == null)
            {
                root = newNode;
            }
            else
            {
                if (newNode.data < root.data)
                    Insert(ref root.Left, newNode);
                else if(newNode.data > root.data)
                    Insert(ref root.Right, newNode);
            }
        }

        protected static bool Traversal(ref TreeNode root, int value)
        {
            bool ret = true;
            while(root != null)
            {
                if (value == root.data)
                {
                    root.flag = 1;
                    break;
                }
                else
                {
                    if(root.flag == 0)
                    {
                        ret = false;
                        break;
                    }
                    if(value < root.data)
                    {
                        ret = Traversal(ref root.Left, value);
                        break;
                    }
                    else
                    {
                        ret = Traversal(ref root.Right, value);
                        break;
                    }
                } 
            }
            return ret;
        }

        protected static void Reset(ref TreeNode root)
        {
            if (root.Left != null)
                Reset(ref root.Left);
            if (root.Right != null)
                Reset(ref root.Right);
            root.flag = 0;
        }
    }

    class Tree_41_Console:Tree_41
    {
        public static new void Execute()
        {
            while (true)
            {
                string init = Console.ReadLine();
                string[] lenN = init.Split(new char[] { ' ' });
                len = int.Parse(lenN[0]);
                if (len == 0)
                    break;
                N = int.Parse(lenN[1]);
                TreeNode root = CreateTree(len);
                for (int i = 0; i < N; i++)
                {
                    string str = Console.ReadLine();
                    string[] element = str.Split(new char[] { ' ' });
                    bool isSame = true;
                    foreach (string str_elem in element)
                    {
                        int value = int.Parse(str_elem);
                        if (isSame)
                            isSame = Traversal(ref root, value);
                    }
                    if (isSame)
                        Console.WriteLine("Yes");
                    else
                        Console.WriteLine("No");
                    Reset(ref root);
                }
            }
        }

        private static TreeNode CreateTree(int len)
        {
            if (len < 1)
                return null;
            string init = Console.ReadLine();
            string[] element = init.Split(new char[] { ' ' });
            TreeNode root = new TreeNode();
            for (int i = 0; i < len; i++)
            {
                if (i == 0)
                    root.data = int.Parse(element[0]);
                else
                {
                    TreeNode node = new TreeNode(int.Parse(element[i]));
                    if (node.data > root.data)
                        Insert(ref root.Right, node);
                    else if (node.data < root.data)
                        Insert(ref root.Left, node);
                }
            }
            return root;
        }
    }

    class TreeNode
    {
        public int data;
        public TreeNode Left = null;
        public TreeNode Right = null;
        public int flag = 0;

        public TreeNode() { }

        public TreeNode(int data)
        {
            this.data = data;
        }
    }
}
