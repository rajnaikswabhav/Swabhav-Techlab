using System;
using System.Collections.Generic;
using System.Text;

namespace StrctureExample
{
    struct Books
    {
        private String title;
        private String author;
        private String subject;
        private int book_id;

        public void getValues(String title, String author, String subject, int book_id)
        {
            this.title = title;
            this.author = author;
            this.subject = subject;
            this.book_id = book_id;
        }

        public void display()
        {
            Console.WriteLine("Title : {0}", title);
            Console.WriteLine("Author : {0}", author);
            Console.WriteLine("Subject : {0}", subject);
            Console.WriteLine("Book_id :{0}", book_id);
        }
    };
    class Program
    {
        static void Main(string[] args)
        {
            Books Book1 = new Books();   
            Books Book2 = new Books();   

            Book1.getValues("C Programming",
            "Nuha Ali", "C Programming Tutorial", 101);

            Book2.getValues("Telecom Billing",
            "Zara Ali", "Telecom Billing Tutorial", 102);

            Book1.display();

            Book2.display();
        }
    }
}
