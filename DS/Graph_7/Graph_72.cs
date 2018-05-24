using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_7
{
    class Graph_72
    {
        private static GameMap map;

        public static void Initial(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string[] str = sr.ReadLine().Split(new char[] { ' ' });
                    //鳄鱼数量
                    int crocs = int.Parse(str[0]);
                    //最大步长
                    int step = int.Parse(str[1]);
                    map = new GameMap(crocs, step);
                    for (int i = 0; i < crocs; i++)
                    {
                        string[] pair = sr.ReadLine().Split(new char[] { ' ' });
                        int x = int.Parse(pair[0]);
                        int y = int.Parse(pair[1]);
                        map.AddCroc(i, new Node(x,y));
                    }
                }
            }
        }

        public static void Do()
        {
            map.LiveOrDead();
        }
    }

    class GameMap
    {
        public static double Radius = 7.5;
        private Player player;
        private Node[] nodeList;

        public GameMap(int size, int playerStep)
        {
            nodeList = new Node[size];
            player = new Player(playerStep);
        }

        public void AddCroc(int idx, Node node)
        {
            nodeList[idx] = node;
        }

        public void LiveOrDead()
        {
            if (player.step + Radius >= 50)
            {
                Console.WriteLine(1);
            }
            else
            {
                int idx = BFS();
                PrintPath(idx);
            }               
        }

        private int BFS()
        {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(new Node(0,0));            
            int lastIdx = -1;
            double minJumpDist = double.MaxValue;
            while (q.Count>0)
            {
                Node start = q.Dequeue();
                player.StartFrom(start);
                int idx = 0;
                foreach(Node node in nodeList)
                {
                    if(!node.visited && player.CanJumpTo(node))
                    {
                        node.visited = true;
                        node.dist = start.dist + 1;
                        node.path = start;
                        q.Enqueue(node);
                        if (node.isSafe(player.step))
                        {
                            double jumpDist = getFirstJumpList(idx);
                            if(jumpDist < minJumpDist)
                            {
                                minJumpDist = jumpDist;
                                lastIdx = idx;
                            }
                        }
                    }
                    idx++;
                }
            }
            return lastIdx;
        }

        private double getFirstJumpList(int lastIdx)
        {
            Node tNode = nodeList[lastIdx];
            while (tNode.path.path != null)
            {
                tNode = tNode.path;
            }
            return tNode.jumpDist;
        }

        private void PrintPath(int lastIdx)
        {
            if (lastIdx < 0)
                Console.WriteLine(0);
            else
            {
                Stack<Node> stack = new Stack<Node>();
                Node tNode = nodeList[lastIdx];
                Console.WriteLine(tNode.dist + 1);
                while (tNode.path != null)
                {
                    stack.Push(tNode);
                    tNode = tNode.path;
                }
                while(stack.Count>0)
                {
                    Node history = stack.Pop();
                    Console.WriteLine(history.X + " " + history.Y);
                }
            }
        }
    }

    class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool visited { get; set; }
        public int dist { get; set; }
        public double jumpDist { get; set; }
        public Node path { get; set; }

        public Node() {
            X = 0;
            Y = 0;
            visited = false;
            dist = 0;
            jumpDist = 0;
        }

        public Node(int x, int y)
        {
            this.X = x;
            this.Y = y;
            visited = false;
            dist = 0;
            jumpDist = 0;
        }

        public bool isSafe(int step)
        {
            if (CanEscape(X,step) || CanEscape(Y,step))
                return true;
            else
                return false;
        }

        private bool CanEscape(int coordinate, int step)
        {
            bool ret = false;
            if (coordinate > 0)
            {
                if (50 - coordinate <= step)
                    ret = true;
            }
            else
            {
                if (coordinate + 50 <= step)
                    ret = true;
            }
            return ret;
        }
    }

    class Player
    {
        private int x = 0;
        private int y = 0;
        public int step { get; set; }

        public Player(int step)
        {
            this.step = step;
        }

        public bool CanJumpTo(Node node)
        {
            bool ret = false;
            int dstX = node.X;
            int dstY = node.Y;
            double maxJump = step;
            if (x == 0 && y == 0)
                maxJump += GameMap.Radius;
            double jumpDist = Math.Pow(x - dstX, 2) + Math.Pow(y - dstY, 2);
            if(jumpDist <= Math.Pow(maxJump, 2) )
            {
                node.jumpDist = jumpDist;
                ret = true;
            }
            return ret;
        }

        public void StartFrom(Node node)
        {
            x = node.X;
            y = node.Y;
        }
    }
}
