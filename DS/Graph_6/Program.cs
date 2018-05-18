using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_6
{
    class Program
    {
        static void Main(string[] args)
        {
            #region DFS与BFS
            //Graph_61.createGraphfromFile("Graph_61.txt");
            //Graph_61.DFS();
            //Graph_61.BFS();;
            #endregion
            #region 拯救007（简单的）
            //Graph_62.gameReady("Graph_62.txt");
            #endregion
            #region 六度空间理论
            Graph_63.initialGraph("Graph_63.txt");
            Graph_63.bfsPlus();
            #endregion
            Console.ReadKey();
        }
    }
}
