using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeApplication.Models;
using PagedList;
using PagedList.Mvc;

namespace EmployeeApplication.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: Employees
        public ActionResult Index(string sortOrder, string searchString, int? pageNumber)
        {
            ViewBag.IDSortParam = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.FNSortParam = sortOrder == "FirstName" ? "fn_desc" : "FirstName";
            ViewBag.LNSortParam = sortOrder == "LastName" ? "ln_desc" : "LastName";
            ViewBag.DesgSortParam = sortOrder == "Designation" ? "desg_desc" : "Designation";
            ViewBag.LocSortParam = sortOrder == "WorkLocation" ? "loc_desc" : "WorkLocation";

            var employees = from emp in db.Employees
                            select emp;
            //filtering
            if(!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(emp => emp.FirstName.Contains(searchString) || emp.LastName.Contains(searchString));
            }
            //sorting
            switch(sortOrder)
            {
                case "id_desc":
                    employees = employees.OrderByDescending(emp => emp.EmployeeID);
                    break;
                case "FirstName":
                    employees = employees.OrderBy(emp => emp.FirstName);
                    break;
                case "fn_desc":
                    employees = employees.OrderByDescending(emp => emp.FirstName);
                    break;
                case "LastName":
                    employees = employees.OrderBy(emp => emp.LastName);
                    break;
                case "ln_desc":
                    employees = employees.OrderByDescending(emp => emp.LastName);
                    break;
                case "Designation":
                    employees = employees.OrderBy(emp => emp.Designation);
                    break;
                case "desg_desc":
                    employees = employees.OrderByDescending(emp => emp.Designation);
                    break;
                case "WorkLocation":
                    employees = employees.OrderBy(emp => emp.WorkLocation);
                    break;
                case "loc_desc":
                    employees = employees.OrderByDescending(emp => emp.WorkLocation);
                    break;
                default:
                    employees = employees.OrderBy(emp => emp.EmployeeID);
                        break;

            }
            //paging- 3 records per page
            return View(employees.ToList().ToPagedList(pageNumber??1,3));
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,FirstName,LastName,Designation,WorkLocation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,FirstName,LastName,Designation,WorkLocation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
