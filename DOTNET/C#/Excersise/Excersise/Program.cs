using System;
using System.Collections.Generic;
using System.Text;

namespace Excersise
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //RefExample();
        }

       /* private static void RefExample()
        {
            var bookCollection = new BookCollection();
            bookCollection.ListBooks();

            ref var book = ref bookCollection.GetBookByTitle("xyz");
            if (book != null)
            {
                book = new Book
                {
                    author = "Akash",
                    title = "abc",
                };
                bookCollection.ListBooks();
            }
        }*/
    }
}
