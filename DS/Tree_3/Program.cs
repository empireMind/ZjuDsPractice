using System;

namespace Tree_3
{
    class Program
    {
        static string txt11 = "test31_1.txt";
        static string txt12 = "test31_2.txt";
        static string txt2 = "test32.txt";
        static string txt3 = "test33.txt";

        static void Main(string[] args)
        {
            #region Tree_31
            //Tree_31 newClass = new Tree_31();
            //int root11 = newClass.CreateTree(ref Tree_31.tree1, txt11);
            //int root12 = newClass.CreateTree(ref Tree_31.tree2, txt12);
            //bool ret = newClass.IsSame(root1, root2);
            //string answer = ret ? "Yes" : "No";
            //Console.WriteLine(answer);
            //Console.ReadKey();
            #endregion

            #region Tree_32
            //Tree_32 newClass32 = new Tree_32();
            //int root2 = newClass32.CreateTree(ref Tree_32.Tree, txt2);
            //newClass32.findLeaves(Tree_32.Tree, root3);
            //Console.ReadKey();
            #endregion

            #region Tree_33
            Tree_33 newClass33 = new Tree_33();
            newClass33.CreatePreAndInOrder(txt3);
            Console.ReadKey();
            #endregion
        }
    }
}
