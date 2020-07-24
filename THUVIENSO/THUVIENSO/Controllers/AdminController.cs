using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using THUVIENSO.Models;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Web.UI;

namespace THUVIENSO.Controllers
{
    
    public class AdminController : Controller
    {
        private THUVIENSO_Entities db = new THUVIENSO_Entities();

        public ActionResult test()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public bool RememberMe { set; get; }

        [HttpPost]

        public ActionResult Login(account accounts)
        {
            if (ModelState.IsValid)
            {
                MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(accounts.passwords);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                accounts.passwords = sb.ToString();

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
                        ViewBag.Message = String.Format("Bạn không có quyền truy cập ở trang web này.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }

            }
            return View(accounts);
        }


        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult Index()
        {
            var accounts = db.accounts.Include(a => a.customer).Include(a => a.Monney).Where(a => a.levels ==2);
            return View(accounts.ToList());
        }

        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult InsertChuDe()
        {
            ViewBag.id = new SelectList(db.books, "id", "booktitle");
            return View();
        }

       
        [Authorize] //check xem đã đăng nhập chưa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertChuDe([Bind(Include = "id,nametopic")] booktopic booktopic)
        {
            if (ModelState.IsValid)
            {  
                db.booktopics.Add(booktopic);
                db.SaveChanges();
               
                ViewBag.message = "Thêm thành công";
                ModelState.Clear();
                return View();  
            }

          //  ViewBag.id = new SelectList(db.books, "id", "booktitle", booktopic.id);
            return View();
        }
        [Authorize] //check xem đã đăng nhập chưa
        [HttpGet]
        public ActionResult InsertBook()
        {
            ViewBag.id = new SelectList(db.booktopics, "id", "nametopic");
            return View();
        }

        [Authorize] //check xem đã đăng nhập chưa
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

                model.price = 100000;
                db.books.Add(model);
                db.SaveChanges();
                ViewBag.Message = String.Format("Thêm sách thành công");               
                return RedirectToAction("ListBook");
            }

            ViewBag.id = new SelectList(db.booktopics, "id", "nametopic", model.id);
            return View();
        }
        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult ListBook()
        {
            var books = db.books.Include(b => b.booktopic);
            return View(books.ToList());
        }
        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult ListBook1(int? id)
        {
            var books = db.books.Include(b => b.booktopic).Where(b => b.id == id);
            return View(books.ToList());
        }
        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [Authorize] //check xem đã đăng nhập chưa
        // POST: books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            book book = db.books.Find(id);
            db.books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("ListBook");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        ///////////////////////////////////////////////////
        ///// GET: customers/Delete/5
        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult Deletea(int? id)
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
        [HttpPost, ActionName("Deletea")]
        [ValidateAntiForgeryToken]
        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult DeleteConfirme(int id)
        {
            account account = db.accounts.Find(id);
            customer customer = db.customers.Find(id);
            Monney monney = db.Monneys.Find(id);
            db.customers.Remove(customer);
            db.Monneys.Remove(monney);
            db.accounts.Remove(account);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: books/Edit/5
        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult Editbook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.booktopics, "id", "nametopic", book.id);
            return View(book);
        }

        // POST: books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult Editbook( book model)
        {
            if (ModelState.IsValid)
            {
               
                //imgData
                string DatafileName = Path.GetFileNameWithoutExtension(model.datafile.FileName);
                string Dataextension = Path.GetExtension(model.datafile.FileName);
                DatafileName = DatafileName + DateTime.Now.ToString("yymmssff") + Dataextension;
                model.DataContent = "~/PDF/" + DatafileName;
                DatafileName = Path.Combine(Server.MapPath("~/PDF"), DatafileName);
                model.datafile.SaveAs(DatafileName);

              
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

              //  ViewBag.message = "Thêm thành sách công";
                return RedirectToAction("ListBook"); 
            }
            ViewBag.id = new SelectList(db.booktopics, "id", "nametopic", model.id);
            return View(model);
        }
        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult ThongTinKhachHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }



    }
}