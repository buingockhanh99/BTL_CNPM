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
    [Authorize] //check xem đã đăng nhập chưa
    public class AdminController : Controller
    {
        private THUVIENSO_Entities db = new THUVIENSO_Entities();
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

        [HttpGet]
        public ActionResult InsertBook()
        {
            ViewBag.id = new SelectList(db.booktopics, "id", "nametopic");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertBook(book model)
        {
            if (ModelState.IsValid)
            {
                //insert Images
                string imgfileName = Path.GetFileNameWithoutExtension(model.imgfile.FileName);
                string extension = Path.GetExtension(model.imgfile.FileName);
                imgfileName = imgfileName + DateTime.Now.ToString("yymmssff") + extension;
                model.img = "~/Image/" + imgfileName;
                imgfileName = Path.Combine(Server.MapPath("~/Image"), imgfileName);
                model.imgfile.SaveAs(imgfileName);

                //imgData
                string DatafileName = Path.GetFileNameWithoutExtension(model.datafile.FileName);
                string Dataextension = Path.GetExtension(model.datafile.FileName);
                DatafileName = DatafileName + DateTime.Now.ToString("yymmssff") + Dataextension;
                model.DataContent = "~/PDF/" + DatafileName;
                DatafileName = Path.Combine(Server.MapPath("~/PDF"), DatafileName);
                model.datafile.SaveAs(DatafileName);

                db.books.Add(model);
                db.SaveChanges();
                return RedirectToAction("ListBook");
            }

            ViewBag.id = new SelectList(db.booktopics, "id", "nametopic", model.id);
            return View(model);
        }

        public ActionResult ListBook()
        {
            var books = db.books.Include(b => b.booktopic);
            return View(books.ToList());
        }



    }
}