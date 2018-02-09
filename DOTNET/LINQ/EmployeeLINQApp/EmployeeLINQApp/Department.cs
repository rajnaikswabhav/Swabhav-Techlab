using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLINQApp
{
    public class Department
    {
        private int deptId;
        private String deptName;
        private String location;

        public Department(int deptId, String deptName, String location)
        {
            this.deptId = deptId;
            this.deptName = deptName;
            this.location = location;
        }
        public int DeptId { get { return deptId; } }
        public String DeptName { get { return deptName; } }
        public String Location { get { return location; } }
    }
}
