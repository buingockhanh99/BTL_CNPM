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
    public class booktopicsController : Controller
    {
        private THUVIENSO_Entities db = new THUVIENSO_Entities();

        // GET: booktopics
        public ActionResult Index()
        {
            return View(db.booktopics.ToList());
        }

        // GET: booktopics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booktopic booktopic = db.booktopics.Find(id);
            if (booktopic == null)
            {
                return HttpNotFound();
            }
            return View(booktopic);
        }

        // GET: booktopics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: booktopics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nametopic")] booktopic booktopic)
        {
            if (ModelState.IsValid)
            {
                db.booktopics.Add(booktopic);
                db.SaveChanges();
                ModelState.Clear();
                return View();
            }

            return View();
        }

        // GET: booktopics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booktopic booktopic = db.booktopics.Find(id);
            if (booktopic == null)
            {
                return HttpNotFound();
            }
            return View(booktopic);
        }

        // POST: booktopics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nametopic")] booktopic booktopic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booktopic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booktopic);
        }

        // GET: booktopics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booktopic booktopic = db.booktopics.Find(id);
            if (booktopic == null)
            {
                return HttpNotFound();
            }
            return View(booktopic);
        }

        // POST: booktopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            booktopic booktopic = db.booktopics.Find(id);
            db.booktopics.Remove(booktopic);
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
