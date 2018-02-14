using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLINQApp
{
    class Program
    {
        private static List<Employee> listOfEmployee = new List<Employee>();
        private static List<Department> listOfDepartment = new List<Department>();
        public static void Main(string[] args)
        {
            Employee employee1 = new Employee(7839, "KING", "PRESIDENT",null, "17-NOV-81", 5000,null, 10);
            Employee employee2 = new Employee(7698, "BLAKE", "MANAGER", 7839, "1-MAY-81", 2850,null, 30);
            Employee employee3 = new Employee(7782, "CLARK", "MANAGER", 7839, "9-JUN-81", 2450,null, 10);
            Employee employee4 = new Employee(7566, "JONES", "MANAGER", 7839, "2-APR-81", 2975, null, 20);
            Employee employee5 = new Employee(7654, "MARTIN", "SALESMAN", 7698, "28-SEP-81", 1250, 1400, 30);
            Employee employee6 = new Employee(7499, "ALLEN", "SALESMAN", 7698, "20-FEB-81", 1600, 300, 30);
            Employee employee7 = new Employee(7844, "TURNER", "SALESMAN", 7698, "8-SEP-81", 1500, 0, 30);
            Employee employee8 = new Employee(7900, "JAMES", "CLERK", 7698, "3-DEC-81", 950, null, 30);
            Employee employee9 = new Employee(7521, "WARD", "SALESMAN", 7698, "22-FEB-81", 1250, 500, 30);
            Employee employee10 = new Employee(7902, "FORD", "ANALYST", 7566, "3-DEC-81", 3000, null, 20);
            Employee employee11 = new Employee(7369, "SMITH", "CLERK", 7902, "17-DEC-80", 800, null, 20);
            Employee employee12 = new Employee(7788, "SCOTT", "ANALYST", 7566, "09-DEC-82", 3000, null, 20);
            Employee employee13 = new Employee(7876, "ADAMS", "CLERK", 7788, "12-JAN-83", 1100, null, 20);
            Employee employee14 = new Employee(7934, "MILLER", "CLERK", 7782, "23-JAN-82", 1300, null, 10);

            listOfEmployee.Add(employee1);
            listOfEmployee.Add(employee2);
            listOfEmployee.Add(employee3);
            listOfEmployee.Add(employee4);
            listOfEmployee.Add(employee5);
            listOfEmployee.Add(employee6);
            listOfEmployee.Add(employee7);
            listOfEmployee.Add(employee8);
            listOfEmployee.Add(employee9);
            listOfEmployee.Add(employee10);
            listOfEmployee.Add(employee11);
            listOfEmployee.Add(employee12);
            listOfEmployee.Add(employee13);
            listOfEmployee.Add(employee14);

            Department dept1 = new Department(10, "ACCOUNTING", "NEW YORK");
            Department dept2 = new Department(20, "RESEARCH", "DALLAS");
            Department dept3 = new Department(30, "SALES", "CHICAGO");
            Department dept4 = new Department(40, "OPERATIONS", "BOSTON");

            listOfDepartment.Add(dept1);
            listOfDepartment.Add(dept2);
            listOfDepartment.Add(dept3);
            listOfDepartment.Add(dept4);
          
            EmployeeQuery();
        }

        public static void EmployeeQuery()
        {
            //var displayByAcendingOrder = listOfEmployee.OrderBy(e => e.EmployeeId);
            //foreach(var employee in displayByAcendingOrder)
            //{
            //    Console.WriteLine(employee.EmployeeId+","+employee.EmployeeName+","+employee.Designation+","+
            //        employee.MGR+","+employee.HireDate+","+employee.Salary+","+employee.Commision+","
            //        +employee.DepartmentNo);
            //}

            //var displayNameInLowerCase = listOfEmployee.ConvertAll(e => new { name = e.EmployeeName.ToLower() });
            //foreach (var employee in displayNameInLowerCase)
            //{
            //    Console.WriteLine(employee.name);
            //}

            //var displayNameSalaryComm = listOfEmployee.Select(e => new {name=e.EmployeeName, salary = e.Salary , comm = e.Commision });
            //foreach (var employee in displayNameSalaryComm)
            //{
            //    Console.WriteLine(employee.name+", "+employee.salary+", "+employee.comm);
            //}

            //var displaySpecificEmployee = listOfEmployee.Where(e => e.DepartmentNo == 10);
            //foreach (var employee in displaySpecificEmployee)
            //{
            //    Console.WriteLine(employee.EmployeeId + "," + employee.EmployeeName + "," + employee.Designation + "," +
            //        employee.MGR+","+employee.HireDate+","+employee.Salary+","+employee.Commision+","
            //        +employee.DepartmentNo);
            //}

            //var displayEmpCommNull = listOfEmployee.Where(e => e.Commision == null);
            //foreach (var employee in displayEmpCommNull)
            //{
            //    Console.WriteLine(employee.EmployeeId + "," + employee.EmployeeName + "," + employee.Designation + "," +
            //        employee.MGR + "," + employee.HireDate + "," + employee.Salary + "," + employee.Commision + ","
            //        + employee.DepartmentNo);
            //}

            //var displayNameSalaryAnnsal = listOfEmployee.Select(e => new
            //{
            //    name = e.EmployeeName,
            //    salary = e.Salary,
            //    annualSalary = e.Salary * 12 +  (e.Commision ?? 0 ) * 12 
            //});
            //foreach (var employee in displayNameSalaryAnnsal)
            //{
            //    Console.WriteLine(employee.name+", "+employee.salary+", "+employee.annualSalary);
            //}

            //var displayDeptEmployee = listOfEmployee.Where(e => e.DepartmentNo == 20 || e.DepartmentNo == 10);
            //foreach (var employee in displayDeptEmployee)
            //{
            //    Console.WriteLine(employee.EmployeeId + "," + employee.EmployeeName + "," + employee.Designation + "," +
            //        employee.MGR+","+employee.HireDate+","+employee.Salary+","+employee.Commision+","
            //        +employee.DepartmentNo);
            //}

            //var displayEmployeeScott = listOfEmployee.Where(e => e.EmployeeName == "SCOTT");
            //foreach (var employee in displayEmployeeScott)
            //{
            //    Console.WriteLine(employee.EmployeeId + "," + employee.EmployeeName + "," + employee.Designation + "," +
            //        employee.MGR + "," + employee.HireDate + "," + employee.Salary + "," + employee.Commision + ","
            //        + employee.DepartmentNo);
            //}

            //var displayTop3EarningEmployee = listOfEmployee.OrderByDescending(e => e.Salary * 12 + (e.Commision ?? 0) * 12).Take(3);
            //foreach (var employee in displayTop3EarningEmployee)
            //{
            //    Console.WriteLine(employee.EmployeeId + "," + employee.EmployeeName + "," + employee.Designation + "," +
            //        employee.MGR + "," + employee.HireDate + "," + employee.Salary + "," + employee.Commision + ","
            //        + employee.DepartmentNo);
            //}

            //var displayYearsOfWorking = listOfEmployee.Select(e => new {
            //    id = e.EmployeeId,
            //    name = e.EmployeeName,
            //    desi = e.Designation,
            //    mgr = e.MGR,
            //    hiredate = e.HireDate,
            //    salary = e.Salary,
            //    comm = e.Commision,
            //    deptNo = e.DepartmentNo,
            //    workingYears =DateTime.Today.Year - DateTime.Parse(e.HireDate).Year
            //});
            //foreach(var employee in displayYearsOfWorking)
            //{
            //    Console.WriteLine(employee.id + "," + employee.name + "," + employee.desi + "," +
            //        employee.mgr + "," + employee.hiredate + "," + employee.salary + "," + employee.comm + ","
            //        + employee.deptNo+","+employee.workingYears);
            //}

            //var displayEmployeeContainS = listOfEmployee.Where(e => e.EmployeeName.Contains("S".ToUpper()));
            //foreach (var employee in displayEmployeeContainS)
            //{
            //    Console.WriteLine(employee.EmployeeId + "," + employee.EmployeeName + "," + employee.Designation + "," +
            //        employee.MGR + "," + employee.HireDate + "," + employee.Salary + "," + employee.Commision + ","
            //        + employee.DepartmentNo);
            //}

            //var displayEmployeesStartWithA = listOfEmployee.Where(e => e.EmployeeName.StartsWith("a".ToUpper()));
            //foreach (var employee in displayEmployeesStartWithA)
            //{
            //    Console.WriteLine(employee.EmployeeId + "," + employee.EmployeeName + "," + employee.Designation + "," +
            //        employee.MGR + "," + employee.HireDate + "," + employee.Salary + "," + employee.Commision + ","
            //        + employee.DepartmentNo);
            //}

            //var displayEmployeeEndWithN = listOfEmployee.Where(e => e.EmployeeName.EndsWith("n".ToUpper()));
            //foreach (var employee in displayEmployeeEndWithN)
            //{
            //    Console.WriteLine(employee.EmployeeId + "," + employee.EmployeeName + "," + employee.Designation + "," +
            //        employee.MGR + "," + employee.HireDate + "," + employee.Salary + "," + employee.Commision + ","
            //        + employee.DepartmentNo);
            //}

            //var displayEmployeesDeptOfScott = listOfEmployee.Where(e => e.DepartmentNo == );
            //foreach (var employee in displayEmployeesDeptOfScott)
            //{
            //    Console.WriteLine(employee.EmployeeId + "," + employee.EmployeeName + "," + employee.Designation + "," +
            //        employee.MGR + "," + employee.HireDate + "," + employee.Salary + "," + employee.Commision + ","
            //        + employee.DepartmentNo);
            //}

            //var displayDepartmentINAscanding = listOfEmployee.OrderBy(n => n.DepartmentNo);
            //foreach (var employee in displayDepartmentINAscanding)
            //{
            //    Console.WriteLine(employee.EmployeeId + "," + employee.EmployeeName + "," + employee.Designation + "," +
            //        employee.MGR + "," + employee.HireDate + "," + employee.Salary + "," + employee.Commision + ","
            //        + employee.DepartmentNo);
            //}

            //var displayUniqueDepartment = listOfEmployee.Select(e => new { num = e.DepartmentNo } ).Distinct().OrderBy(e => e.num);
            //foreach (var employee in displayUniqueDepartment)
            //{
            //    Console.WriteLine(employee.num);
            //}

            //var displayDateFormat = listOfEmployee.Select(e => new {
            //    date = DateTime.Parse(e.HireDate)
            //});
            //foreach (var employee in displayDateFormat)
            //{
            //    Console.WriteLine(employee.date);
            //}

            //var numberOFEmployees = listOfEmployee.Count();
            //Console.WriteLine(numberOFEmployees);

            //var displayNoEmployeesInDept = listOfEmployee.Where(e => e.DepartmentNo == 10).Count();
            //Console.WriteLine(displayNoEmployeesInDept);

            //var sumOfTotalSalary = listOfEmployee.Sum(e => e.Salary);
            //Console.WriteLine(sumOfTotalSalary);

            //var displayEmployeeNameDeptName = (from emp in listOfEmployee join dept in listOfDepartment 
            //                                   on emp.DepartmentNo equals dept.DeptId
            //                                   orderby emp.DepartmentNo
            //                                   select new
            //                                   {  emp.EmployeeName , dept.DeptName }).ToList();
            //foreach(var detail in displayEmployeeNameDeptName)
            //{
            //    Console.WriteLine(detail.EmployeeName+","+detail.DeptName);
            //}

            //var diplayAllDepartments = (from dept in listOfDepartment
            //                            join emp in listOfEmployee
            //                            on dept.DeptId equals emp.DepartmentNo
            //                            select new { dept.DeptName , emp.EmployeeName });
            //foreach(var detail in diplayAllDepartments)
            //{
            //    Console.WriteLine(detail.DeptName+","+detail.EmployeeName);
            //}

            var displayDepartmentThereIsNoEmp = (from dept in listOfDepartment
                                                 join emp in listOfEmployee
                                                 on dept.DeptId equals emp.DepartmentNo
                                                 where dept.DeptId
                                                  )

        }
    }
}
