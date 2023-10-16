using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using system_developer_exam.Data;
using system_developer_exam.Models;

namespace system_developer_exam.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Employee> objEmployeeList = _db.Employees;
            return View(objEmployeeList);
        }
        [HttpPost]
        public IActionResult GetEmployees()
        {
            var employees = _db.Employees.FromSqlRaw($"GetAllEmployees");
            return Json(employees.ToList());
        }


        //GET
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Employee obj)
        {
            if (ModelState.IsValid)
            {
                    _db.Database.ExecuteSqlRaw("InsertEmployee @p0, @p1, @p2, @p3",
                   obj.FirstName, obj.MiddleName, obj.LastName, obj.DateHired);

                    TempData["success"] = "Employee Created Successfully";
                    return RedirectToAction("Index");


            }

            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var employeeFromDb = _db.Employees.Find(id);
            if (employeeFromDb == null)
            {
                return NotFound();
            }

            return View(employeeFromDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee obj)
        {
            if (ModelState.IsValid)
            {
                _db.Database.ExecuteSqlRaw("EditEmployee @p0, @p1, @p2, @p3, @p4",
                obj.Id, obj.FirstName, obj.MiddleName, obj.LastName, obj.DateHired);
                TempData["success"] = "Edit Successfull";
                return RedirectToAction("Index");

            }
            return View(obj);

        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var employeeFromDb = _db.Employees.Find(id);
            if (employeeFromDb == null)
            {
                return NotFound();
            }

            return View(employeeFromDb);
        }
        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Employees.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Database.ExecuteSqlRaw("DeleteEmployee @p0", id);
            TempData["success"] = "Delete Successfull";
            return RedirectToAction("Index");

        }

    }
}

