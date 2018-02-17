using EmployeeMVCApp.Models;
using EmployeeMVCApp.Services;
using EmployeeMVCApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeMVCApp.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeService empService = new EmployeeService();

        public ActionResult Index()
        {
            var vm = new IndexViewModel();
            vm.Employees = empService.GetEmp();
            vm.EmpCount = vm.Employees.Count();
            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var evm = new EditViewModel();
            evm.Emp = empService.Get(id);

            return View(evm);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel vm, int? id)
        {
            if (id == null)
            {
                return View(vm);
            }
            else
            {
                empService.Update(vm.Emp);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Add()
        {
            var avm = new AddViewModel();
            return View(avm);
        }

        [HttpPost]
        public ActionResult Add(AddViewModel avm)
        {
            if (avm.Id == null)
            {
                return View(avm);
            }
            else
            {
                Employee emp = new Employee(avm.Id, avm.Name, avm.Salary, avm.DeptNo);
                empService.Add(emp);
                return View();
            }
        }

        public ActionResult Delete(int? id)
        {
            empService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Search(string name)
        {
            var searchEmp = empService.Search(name);
            return Json(searchEmp,JsonRequestBehavior.AllowGet);
        }
    }
}