using System;
using System.Collections.Generic;
using System.Text;

namespace Excersise
{
    class Book
    {
        public string author;
        public string title;
    }

    public class BookCollection
    {
        private Book[] books = {
            new Book
            {
                author = "abc",
                title = "xyz"
            },
            new Book {
                author = "pqr",
                title = "A book"
            }
        };

        public ref Book GetBookByTitle(string title)
        {
            for(int i=0; i < books.Length; i++)
            {
                if(title == books[i].title)
                {
                    return ref books[i];  
                }
            }
            return null;
        }

        public void ListBooks()
        {
            foreach(var book in books)
            {
                Console.WriteLine(book.title+" by "+book.author);
            }
            Console.WriteLine();
        }
    }
}
