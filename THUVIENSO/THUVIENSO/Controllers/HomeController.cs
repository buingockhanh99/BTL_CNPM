using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace THUVIENSO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

  
        public ActionResult test1()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            //xử lý upload
            file.SaveAs(Server.MapPath("~/images" + file.FileName));
            return "/images/" + file.FileName;
        }
        public ActionResult KhoaHoc()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult test3()
        {
           
            return View();
        }



    }
}