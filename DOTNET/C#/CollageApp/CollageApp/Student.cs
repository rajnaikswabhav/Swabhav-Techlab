using System;

namespace CollageApp
{
    public enum Branch
    {
        COMPUTER, INFORMATIONTECHNOLOGY, CIVIL, ELECTRIC,
        MECHANICAL, AUTOMOBIL, CHEMICAL, ARONOTICAL

    }
    public class Student : Person
    {
        private Branch branch;
        public Student(int id, string address, string dob, Branch branch) : base(id, address, dob)
        {
            this.branch = branch;
        }

        public Branch Branch { get { return branch; } }

        public override string ToString()
        {
            String details = "Student Details....\n";
            details += "Id: " + Id + "\n";
            details += "Address: " + Address + "\n";
            details += "DOB: " + Dob + "\n";
            details += "Branch: " + Branch + "\n";
            return details;
        }
    }
}
