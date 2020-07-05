using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THUVIENSO.Models;

namespace THUVIENSO.Controllers
{
    public class themebooksController : Controller
    {
        private THUVIENSOContext db = new THUVIENSOContext();

        // GET: themebooks
        public ActionResult Index()
        {
            var themebooks = db.themebooks.Include(t => t.book);
            return View(themebooks.ToList());
        }

        // GET: themebooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            themebook themebook = db.themebooks.Find(id);
            if (themebook == null)
            {
                return HttpNotFound();
            }
            return View(themebook);
        }

        // GET: themebooks/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.books, "id", "namebook");
            return View();
        }

        // POST: themebooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nametheme")] themebook themebook)
        {
            if (ModelState.IsValid)
            {
                db.themebooks.Add(themebook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.books, "id", "namebook", themebook.id);
            return View(themebook);
        }

        // GET: themebooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            themebook themebook = db.themebooks.Find(id);
            if (themebook == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.books, "id", "namebook", themebook.id);
            return View(themebook);
        }

        // POST: themebooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nametheme")] themebook themebook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(themebook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.books, "id", "namebook", themebook.id);
            return View(themebook);
        }

        // GET: themebooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            themebook themebook = db.themebooks.Find(id);
            if (themebook == null)
            {
                return HttpNotFound();
            }
            return View(themebook);
        }

        // POST: themebooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            themebook themebook = db.themebooks.Find(id);
            db.themebooks.Remove(themebook);
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
