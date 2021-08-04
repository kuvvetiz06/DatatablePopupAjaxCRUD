using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatatablePopupAjaxCRUD.Models;

namespace DatatablePopupAjaxCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            
            using (DBModel db = new DBModel())
            {
                List<Employee> empList = db.Employee.ToList<Employee>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {          
            return View(new Employee());
            }
            else
            {
                using(DBModel db = new DBModel())
                {
                    return View(db.Employee.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            using(DBModel db = new DBModel())
            {
                if (emp.EmployeeID == 0)
                { 
                db.Employee.Add(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                Employee emp = db.Employee.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                db.Employee.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
        }
    }
}