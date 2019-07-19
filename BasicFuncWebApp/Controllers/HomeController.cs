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
            StudentModel stuModel = DBHelper.getStudentModelByID(sid);
            return View(stuModel);
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