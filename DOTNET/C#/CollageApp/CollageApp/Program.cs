using System;

namespace CollageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var prof = new Professor(110, "Borovali,Mumbai", "20/04/1967");
            prof.CalculateSalary(23);

            var student = new Student(1001, "Varli,Mumbai", "14/10/1997",
                Branch.INFORMATIONTECHNOLOGY);
            Professor prof2 = new Professor(102, "Malad,Mumbai", "31/05/1972");
            prof2.CalculateSalary(28);
            Student student2 = new Student(1002, "Virar,Mumbai", "19/12/1996", Branch.AUTOMOBIL);

            Console.WriteLine(prof);
            Console.WriteLine(student);
            Console.WriteLine(prof2);
            Console.WriteLine(student2);
        }
    }
}
