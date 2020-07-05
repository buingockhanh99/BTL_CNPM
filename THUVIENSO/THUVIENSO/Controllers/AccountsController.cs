using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using THUVIENSO.Models;

namespace THUVIENSO.Controllers
{
    public class AccountsController : Controller
    {
        private THUVIENSOContext db = new THUVIENSOContext();

        // GET: Accounts
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.Information).Include(a => a.Monney);
            return View(accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.Information, "id", "username");
            ViewBag.id = new SelectList(db.Monneys, "id", "id");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,accountname,passwords,levels")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.Information, "id", "username", account.id);
            ViewBag.id = new SelectList(db.Monneys, "id", "id", account.id);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.Information, "id", "username", account.id);
            ViewBag.id = new SelectList(db.Monneys, "id", "id", account.id);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,accountname,passwords,levels")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.Information, "id", "username", account.id);
            ViewBag.id = new SelectList(db.Monneys, "id", "id", account.id);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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

        public ActionResult registration()
        {
            ViewBag.id = new SelectList(db.Information, "id", "username");
            ViewBag.id = new SelectList(db.Monneys, "id", "id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult registration([Bind(Include = "id,accountname,passwords,levels")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.levels = 2;
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.Information, "id", "username", account.id);
            ViewBag.id = new SelectList(db.Monneys, "id", "id", account.id);
            return View(account);
        }



        public ActionResult Login()
        {
            return View();
        }

        public bool RememberMe { set; get; }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account accounts)
        {
            var ketqua = from s in db.Accounts
                         where s.accountname == accounts.accountname
                         && s.passwords == accounts.passwords
                         select s;
            if (ketqua.Any())
            {

                FormsAuthentication.SetAuthCookie(accounts.accountname, RememberMe);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            }
            return View(accounts);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
