using System;
using System.Collections.Generic;
using System.Text;

namespace IndexerExample
{
    class SimpleCollection<T>
    {
        private T[] arra = new T[100];

        public  T this[int i]
        {
            get
            {
                return arra[i];
            }
            set
            {
                arra[i] = value;
            }
        }
    }
}
