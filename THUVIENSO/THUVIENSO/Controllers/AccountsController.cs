using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using THUVIENSO.Models;

namespace THUVIENSO.Controllers
{
    public class accountsController : Controller
    {
        private THUVIENSOEntities db = new THUVIENSOEntities();

        // GET: accounts
        public ActionResult Index()
        {
            var list = from s in db.booktopics
                       join x in db.books on s.id equals x.id
                       select new
                       {
                           s.nametopic,
                           x.authorname
                       };
            ViewBag.abc = list;
            return View();
                       
         //   var accounts = db.accounts.Include(a => a.customer).Include(a => a.Monney);
        //    return View(accounts.ToList());
        }

        // GET: accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: accounts/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.customers, "id", "username");
            ViewBag.id = new SelectList(db.Monneys, "id", "id");
            return View();
        }

        // POST: accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "accountname,passwords,id,levels")] account account)
        {
            if (ModelState.IsValid)
            {
                db.accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.customers, "id", "username", account.id);
            ViewBag.id = new SelectList(db.Monneys, "id", "id", account.id);
            return View(account);
        }

        // GET: accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.customers, "id", "username", account.id);
            ViewBag.id = new SelectList(db.Monneys, "id", "id", account.id);
            return View(account);
        }

        // POST: accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "accountname,passwords,id,levels")] account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.customers, "id", "username", account.id);
            ViewBag.id = new SelectList(db.Monneys, "id", "id", account.id);
            return View(account);
        }

        // GET: accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            account account = db.accounts.Find(id);
            db.accounts.Remove(account);
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


        //-----------------------------------------------------------------------------------//
        public ActionResult registration()
        {
            ViewBag.id = new SelectList(db.customers, "id", "username");
            ViewBag.id = new SelectList(db.Monneys, "id", "id");
            return View();
        }

        // POST: accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult registration([Bind(Include = "accountname,passwords,id,levels")] account account)
        {
            if (ModelState.IsValid)
            {
                account.levels = 2;
                db.accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.customers, "id", "username", account.id);
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
        public ActionResult Login(account accounts)
        {
            var ketqua = from s in db.accounts
                         where s.accountname == accounts.accountname
                         && s.passwords == accounts.passwords
                         select s;


            if (ketqua.Any())
            {
                var quyen = from s in db.accounts
                            where s.accountname == accounts.accountname
                            && s.passwords == accounts.passwords && s.levels == 1
                            select s;
                if (quyen.Any())
                {
                    FormsAuthentication.SetAuthCookie(accounts.accountname, RememberMe);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(accounts.accountname, RememberMe);
                    return RedirectToAction("Index", "KhachHang");
                }
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
