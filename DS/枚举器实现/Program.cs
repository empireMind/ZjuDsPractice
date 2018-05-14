using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 枚举器实现
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] intList = { 1, 2, 3 };
            MyList<int> myIntList = new MyList<int>(intList);
            foreach (int elem in myIntList)
            {
                Console.WriteLine(elem);
            }

            string[] strList = { "一", "二", "三" };
            MyList2<string> myStrList = new MyList2<string>(strList);
            myStrList.Add("四");
            foreach (string elem in myStrList)
            {
                Console.WriteLine(elem);
            }

            double[] doubleList = { 1.1d, 2.2d, 3.3d };
            MyList2<double> myDList = new MyList2<double>(doubleList);
            myDList.Add(4.4d);
            foreach (double elem in myDList)
            {
                Console.WriteLine(elem);
            }


            Console.ReadKey();
        }
    }
}
