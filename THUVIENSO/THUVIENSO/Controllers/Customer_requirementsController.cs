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
    public class Customer_requirementsController : Controller
    {
        private THUVIENSO_Entities db = new THUVIENSO_Entities();

        // GET: Customer_requirements
        public ActionResult Index()
        {
            var customer_requirements = db.Customer_requirements.Include(c => c.customer);
            return View(customer_requirements.ToList());
        }

        // GET: Customer_requirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_requirements customer_requirements = db.Customer_requirements.Find(id);
            if (customer_requirements == null)
            {
                return HttpNotFound();
            }
            return View(customer_requirements);
        }

        // GET: Customer_requirements/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.customers, "id", "username");
            return View();
        }

        // POST: Customer_requirements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id1,Request,statuss")] Customer_requirements customer_requirements)
        {
            if (ModelState.IsValid)
            {
                db.Customer_requirements.Add(customer_requirements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.customers, "id", "username", customer_requirements.id);
            return View(customer_requirements);
        }

        // GET: Customer_requirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_requirements customer_requirements = db.Customer_requirements.Find(id);
            if (customer_requirements == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.customers, "id", "username", customer_requirements.id);
            return View(customer_requirements);
        }

        // POST: Customer_requirements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id1,Request,statuss")] Customer_requirements customer_requirements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_requirements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.customers, "id", "username", customer_requirements.id);
            return View(customer_requirements);
        }

        // GET: Customer_requirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_requirements customer_requirements = db.Customer_requirements.Find(id);
            if (customer_requirements == null)
            {
                return HttpNotFound();
            }
            return View(customer_requirements);
        }

        // POST: Customer_requirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer_requirements customer_requirements = db.Customer_requirements.Find(id);
            db.Customer_requirements.Remove(customer_requirements);
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
