using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEFApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EmpDbContext context = new EmpDbContext();
            //Case1(context);\

            //var displayAllEmpInAsc = context.EMP.Select(e => e).OrderBy(e => e.EName).ToList();
            //foreach(var e in displayAllEmpInAsc)
            //{
            //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",e.Id,e.EName,e.Designation,e.MGR,e.HireDate,e.Salary,e.Commision,e.DeptId);
            //}

            //var displayEmpNameInLower = context.EMP.Select(e => e.EName.ToLower()).ToList();
            //foreach(var e in displayEmpNameInLower)
            //{
            //    Console.WriteLine("{0}",e.ToLower());
            //}

            //var displayNameSalaryCommision = context.EMP.Select(e => new
            //{
            //    name = e.EName,
            //    salary = e.Salary,
            //    comm = e.Commision
            //}).ToList();
            //foreach(var e in displayNameSalaryCommision)
            //{
            //    Console.WriteLine("{0}, {1}, {2}",e.name,e.salary,e.comm);
            //}

            //var displaySpecificEmployee = context.EMP.Where(e => e.DeptId == 10).ToList();
            //foreach (var e in displaySpecificEmployee)
            //{
            //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", e.Id, 
            //        e.EName, e.Designation, e.MGR, e.HireDate, e.Salary, e.Commision, e.DeptId);
            //}

            //var displayEmpCommNull = context.EMP.Where(e => e.Commision == null).ToList();
            //foreach (var e in displayEmpCommNull)
            //{
            //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", e.Id,
            //        e.EName, e.Designation, e.MGR, e.HireDate, e.Salary, e.Commision, e.DeptId);
            //}

            //var displayNameSalaryAnnsal = context.EMP.Select(e => new
            //{
            //    name = e.EName,
            //    salary = e.Salary,
            //    annualSalary = e.Salary * 12 + (e.Commision ?? 0) * 12
            //}).ToList();
            //foreach (var e in displayNameSalaryAnnsal)
            //{
            //    Console.WriteLine("{0}, {1}, {2}",e.name,e.salary,e.annualSalary);
            //}

            //var displayDeptEmployee = context.EMP.Where(e => e.DeptId == 20 || e.DeptId == 10).ToList();
            //foreach (var e in displayDeptEmployee)
            //{
            //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", e.Id,
            //        e.EName, e.Designation, e.MGR, e.HireDate, e.Salary, e.Commision, e.DeptId);
            //}

            //var displayEmployeeScott = context.EMP.Where(e => e.EName == "SCOTT").ToList();
            //foreach (var e in displayEmployeeScott)
            //{
            //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", e.Id,
            //        e.EName, e.Designation, e.MGR, e.HireDate, e.Salary, e.Commision, e.DeptId);
            //}

            //var displayTop3EarningEmployee = context.EMP.OrderByDescending(e => e.Salary * 12 + (e.Commision ?? 0) * 12).Take(3)
            //                                    .ToList();
            //foreach (var e in displayTop3EarningEmployee)
            //{
            //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", e.Id,
            //        e.EName, e.Designation, e.MGR, e.HireDate, e.Salary, e.Commision, e.DeptId);
            //}

            //var displayYearsOfWorking = context.EMP.Select(e => new
            //{
            //    id = e.Id,
            //    name = e.EName,
            //    desi = e.Designation,
            //    mgr = e.MGR,
            //    hiredate = e.HireDate,
            //    salary = e.Salary,
            //    comm = e.Commision,
            //    deptNo = e.DeptId,
            //    workingYears = DateTime.Today.Year - e.HireDate.Year
            //}).ToList();
            //foreach (var e in displayYearsOfWorking)
            //{
            //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}", e.id,
            //        e.name, e.desi, e.mgr, e.hiredate, e.salary, e.comm, e.deptNo, e.workingYears);
            //}

            //var displayEmployeeContainS = context.EMP.Where(e => e.EName.Contains("s".ToUpper())).ToList();
            //foreach (var e in displayEmployeeContainS)
            //{
            //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", e.Id,
            //        e.EName, e.Designation, e.MGR, e.HireDate, e.Salary, e.Commision, e.DeptId);
            //}

            //var displayEmployeesStartWithA = context.EMP.Where(e => e.EName.StartsWith("a".ToUpper())).ToList();
            //foreach (var e in displayEmployeesStartWithA)
            //{
            //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", e.Id,
            //        e.EName, e.Designation, e.MGR, e.HireDate, e.Salary, e.Commision, e.DeptId);
            //}

            //var displayEmployeeEndWithN = context.EMP.Where(e => e.EName.EndsWith("n".ToUpper())).ToList();
            //foreach (var e in displayEmployeeEndWithN)
            //{
            //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", e.Id,
            //        e.EName, e.Designation, e.MGR, e.HireDate, e.Salary, e.Commision, e.DeptId);
            //}

            //var displayDepartmentINAscanding = context.EMP.OrderBy(n => n.DeptId).ToList();
            //foreach (var e in displayDepartmentINAscanding)
            //{
            //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", e.Id,
            //        e.EName, e.Designation, e.MGR, e.HireDate, e.Salary, e.Commision, e.DeptId);
            //}

            //var displayUniqueDepartment = context.EMP.Select(e => new { num = e.DeptId })
            //    .Distinct().OrderBy(e => e.num);
            //foreach (var e in displayUniqueDepartment)
            //{
            //    Console.WriteLine(e.num);
            //}

            //var displayDateFormat = context.EMP.Select(e => new
            //{
            //    date = e.HireDate
            //}).ToList();
            //foreach (var e in displayDateFormat)
            //{
            //    Console.WriteLine(e.date);
            //}

            //var numberOFEmployees = context.EMP.ToList().Count();
            //Console.WriteLine(numberOFEmployees);

            //var displayNoEmployeesInDept = context.EMP.Where(e => e.DeptId == 10).ToList().Count();
            //Console.WriteLine(displayNoEmployeesInDept);

            //var sumOfTotalSalary = context.EMP.ToList().Sum(e => e.Salary);
            //Console.WriteLine(sumOfTotalSalary);

            //var displayEmployeeNameDeptName = (from emp in context.EMP
            //                                   join dept in context.DEPT
            //        on emp.DeptId equals dept.Id
            //                                   orderby emp.DeptId
            //                                   select new
            //                                   { emp.EName, dept.DName }).ToList();
            //foreach (var detail in displayEmployeeNameDeptName)
            //{
            //    Console.WriteLine(detail.EName + "," + detail.DName);
            //}

            //var diplayAllDepartments = (from dept in context.DEPT
            //                            join emp in context.EMP
            //                            on dept.Id equals emp.DeptId
            //                            select new { dept.DName, emp.EName }).ToList();
            //foreach (var detail in diplayAllDepartments)
            //{
            //    Console.WriteLine(detail.DName + "," + detail.EName);
            //}

            var displayEmpNameBossName = (from emp in context.EMP
                                          let Boss = emp.EName
                                          let Emp = emp.EName
                                          let BossId = emp.Id
                                          let EmpId = emp.MGR
                                          where BossId == EmpId
                                          select new { Boss, Emp }).ToList();
            foreach (var detail in displayEmpNameBossName)
            {
                Console.WriteLine(detail.Boss + "," + detail.Emp);
            }
        }

        private static void Case1(EmpDbContext context)
        {
            Department department = new Department();
            Employee employee = new Employee();

            context.EMP.Add(employee);
            context.DEPT.Add(department);
        }
    }
}
