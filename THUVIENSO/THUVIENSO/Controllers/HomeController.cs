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

        

        public ActionResult ViewBook(int? id)
        {
            var select = from s in db.books
                         where s.id == id
                         select s;
            return View(select.ToList());
        }
        [HttpPost]
        public ActionResult Search_Results(book model)
        {
            /*  var select = from s in db.books
                            where s.booktitle == searchString && s.authorname == searchString
                            select s;*/
          
            var books = db.books.Where(b => b.booktitle.Contains(model.booktitle));
            return View(books.ToList());
        }




    }
}