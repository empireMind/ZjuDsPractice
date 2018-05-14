using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_5
{
    class Tree_53
    {
        protected static int len;
        protected static char[] ch_arr;
        protected static int[] weight;
        protected static int stuNum;
        protected static Answer[][] stuAnswer;
        
        public static void Init(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    len = int.Parse(sr.ReadLine());

                    ch_arr = new char[len];
                    weight = new int[len];
                    string[] str = sr.ReadLine().Split(new char[] { ' ' });
                    for(int i=0; i<len; i++)
                    {
                        ch_arr[i] = char.Parse(str[2*i]);
                        weight[i] = int.Parse(str[2*i+1]);
                    }

                    stuNum = int.Parse(sr.ReadLine());

                    stuAnswer = new Answer[stuNum][];
                    for(int i=0; i<stuNum; i++)
                    {
                        Answer[] someStu = new Answer[len];
                        for(int j=0; j<len; j++)
                        {
                            string[] pair = sr.ReadLine().Split(new char[] { ' ' });
                            someStu[j] = new Answer(char.Parse(pair[0]), pair[1]);
                        }
                        stuAnswer[i] = someStu;
                    } 
                }
            }
            ;
        }         
    }

    class Answer
    {
        public char data;
        public string answer;

        public Answer() { }

        public Answer(char data, string answer)
        {
            this.data = data;
            this.answer = answer;
        }
    }
}
