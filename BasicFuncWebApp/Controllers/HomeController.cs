using BasicFuncWebApp.Helper;
using BasicFuncWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicFuncWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<StudentModel> studentModels;
            studentModels = DBHelper.getStudentModels();
            return View(studentModels);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                StudentModel student = DBHelper.addStudentToDB(studentModel);
                // TODO: handel the case below
                if (student.sid <= 0)
                {
                    // this means student is not inserted
                }
                else if (student.sid >= 1)
                {
                    // this might mean that student is inserted
                    return RedirectToAction("details", new { id = student.sid });
                }
            }
            return View();
        }

        public ActionResult Details(string id)
        {
            int sid = 0;
            bool isInt = int.TryParse(id, out sid);
            if (isInt == false || sid < 0)
            {
                return View(new StudentModel { sid = 0, email = "example@example.com", firstName = "Not provided", lastName = "Not provided" });
            }
            StudentModel stuModel = DBHelper.getStudentModelByID(sid);
            return View(stuModel);
        }

        public ActionResult Edit(string id)
        {
            int sid = 0;
            bool isInt = int.TryParse(id, out sid);
            if (isInt == false || sid < 0)
            {
                return View(new StudentModel { sid = 0, email = "example@example.com", firstName = "Not provided", lastName = "Not provided" });
            }

            // TODO: Remove the showAlert form TempData when used
            if (TempData["showAlert"] != null)
            {
                ViewBag.ShowAlertDanger = true;
                ViewBag.AlertMessage = TempData["showAlert"].ToString();
            }
            StudentModel stuModel = DBHelper.getStudentModelByID(sid);
            return View(stuModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentModel student)
        {
            if (ModelState.IsValid)
            {
                int noOfRowsEffected = DBHelper.updateStudentModel(student);
                if (noOfRowsEffected == 1)
                {
                    // this means only one row is effected which is what was supposed to happen in first place
                    // now move him/her to details page to show them new details
                    return RedirectToAction("details", new { id = student.sid });
                }
                else if (noOfRowsEffected <= 0)
                {
                    //this means nothing has been updated
                    TempData["showAlert"] = "Could not update the record !";
                    return RedirectToAction("edit", new { id = student.sid });
                }
            }
            TempData["showAlert"] = "Could not update the record !";
            return RedirectToAction("edit", new { id = student.sid });
        }

        public ActionResult Delete(int id)
        {
            int noOfRowsEffected = DBHelper.deleteStudentModel(id);
            if (noOfRowsEffected == 1)
            {
                // this means deleted successfully
            }
            return RedirectToAction("index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
