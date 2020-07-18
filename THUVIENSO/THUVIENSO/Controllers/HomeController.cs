using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using THUVIENSO.Models;
using System.IO;


namespace THUVIENSO.Controllers
{
    public class HomeController : Controller
    {
        private THUVIENSO_Entities db = new THUVIENSO_Entities();

        public ActionResult Index()
        {
            var books = db.books.Include(b => b.booktopic);
            return View(books.ToList());
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
        
        public ActionResult test3()
        {
           
            return View();
        }

        public ActionResult ViewBook()
        {
            var books = db.books.Include(b => b.booktopic);
            return View(books.ToList());
        }



    }
}