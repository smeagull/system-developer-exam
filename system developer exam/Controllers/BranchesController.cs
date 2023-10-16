using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using system_developer_exam.Data;
using system_developer_exam.Models;

namespace system_developer_exam.Controllers
{
    public class BranchesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BranchesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Branch> objBranchList = _db.Branches;
            return View(objBranchList);
        }
        [HttpPost]
        public IActionResult GetBranches()
        {
            var objBranches = _db.Branches.FromSqlRaw($"GetAllBranches");
            return Json(objBranches.ToList());
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Branch obj)
        {
            if (ModelState.IsValid)
            {
                _db.Database.ExecuteSqlRaw("InsertBranch @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8",
                obj.PermitNumber, obj.BranchManager, obj.BranchName, obj.DateOpened, obj.IsActive, obj.BranchCode, obj.Address, obj.Barangay, obj.City);
                TempData["success"] = "Branch Created Successfull";
                return RedirectToAction("Index");

            }
            return View(obj);

        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var branchFromDb = _db.Branches.Find(id);
            if (branchFromDb == null)
            {
                return NotFound();
            }

            return View(branchFromDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Branch obj)
        {
            if (ModelState.IsValid)
            {
                _db.Database.ExecuteSqlRaw("EditBranch @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9",
                obj.Id, obj.PermitNumber, obj.BranchManager, obj.BranchName, obj.DateOpened, obj.IsActive, obj.BranchCode, obj.Address, obj.Barangay, obj.City);
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
            var branchFromDb = _db.Branches.Find(id);
            if (branchFromDb == null)
            {
                return NotFound();
            }

            return View(branchFromDb);
        }
        //Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Branches.Find(id); 
            if (obj == null)
            {
                return NotFound();
            }
            //_db.Remove(obj);
            //_db.SaveChanges();
            _db.Database.ExecuteSqlRaw("DeleteBranch @p0", id);
            TempData["success"] = "Delete Successfull";
            return RedirectToAction("Index");

        }
        
    }
}

