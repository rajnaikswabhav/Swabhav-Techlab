using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEFApp
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string EName  { get; set; }
        public string Designation  { get; set; }
        public int? MGR  { get; set; }
        public DateTime HireDate  { get; set; }
        public double Salary  { get; set; }
        public double? Commision  { get; set; }
        public int DeptId { get; set; }
    }
}
