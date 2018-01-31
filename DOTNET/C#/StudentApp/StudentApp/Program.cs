using System;

namespace StudentApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var studentConsole = new StudentConsole();
           studentConsole.AddStudent();
           studentConsole.showData();
            

        }
    }
}
