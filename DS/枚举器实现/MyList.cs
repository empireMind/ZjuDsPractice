using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 枚举器实现
{
    class MyList<T>:IEnumerable
    {
        private ArrayList mylist= new ArrayList();
        public MyList(T[] inputList)
        {
            foreach(T elem in inputList)
            {
                mylist.Add(elem);
            }
        }

        public void Add(T newElem)
        {
            mylist.Add(newElem);
        }

        public IEnumerator GetEnumerator()
        {
            return new MyListEnumerator(this);
        }

        private class MyListEnumerator : IEnumerator
        {
            private int idx = 0;
            private MyList<T> datalist;

            public MyListEnumerator(MyList<T> datalist)
            {
                this.datalist = datalist;
            }

            public object Current
            {
                get
                {
                    return datalist.mylist[idx++];
                }
            }

            public bool MoveNext()
            {
                if (idx < datalist.mylist.Count)
                    return true;
                else
                    return false;
            }

            public void Reset()
            {
                idx = -1; 
            }
        }

    }

    class MyList2<T>:IEnumerable<T>
    {
        private ArrayList list = new ArrayList();

        public MyList2(T[] list)
        {
            foreach(T elem in list)
            {
                this.list.Add(elem);
            }
        }

        public void Add(T newElem)
        {
            list.Add(newElem);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(T elem in list)
            {
                yield return elem;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
