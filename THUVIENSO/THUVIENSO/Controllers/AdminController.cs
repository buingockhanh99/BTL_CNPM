using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using THUVIENSO.Models;

namespace THUVIENSO.Controllers
{
    [Authorize] //check xem đã đăng nhập chưa
    public class AdminController : Controller
    {
        private THUVIENSOEntities db = new THUVIENSOEntities();
        // GET: Admin
        public ActionResult Index()
        {
            var books = db.books.Include(b => b.booktopic);
            return View(books.ToList());
           
        }


        public ActionResult InsertChuDe()
        {
            ViewBag.id = new SelectList(db.books, "id", "booktitle");
            return View();
        }

        // POST: booktopics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertChuDe([Bind(Include = "id,nametopic")] booktopic booktopic)
        {
            if (ModelState.IsValid)
            {
                db.booktopics.Add(booktopic);
                db.SaveChanges();
                ViewBag.message = "Thêm thành công";
                return View();
                // return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.books, "id", "booktitle", booktopic.id);
            return View(booktopic);
        }


        public ActionResult InsertBook()
        {
            ViewBag.id = new SelectList(db.booktopics, "id", "nametopic");
            return View();
        }

        // POST: books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertBook([Bind(Include = "id,booktitle,authorname,content,price")] book book)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.booktopics, "id", "nametopic", book.id);
            return View(book);
        }

    }
}