using System;
using System.Collections.Generic;
using System.Text;

namespace IndexerExample
{
    class SimpleCollection<T>
    {
        private T[] arry = new T[100];

        public  T this[int i]
        {
            get
            {
                return arry[i];
            }
            set
            {
                arry[i] = value;
            }
        }
    }
}
