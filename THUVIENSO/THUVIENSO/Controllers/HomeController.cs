using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using THUVIENSO.Models;

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


        //-----------------------------------------------------------------------------------//
        public ActionResult registration()
        {
            return View();
        }

        // POST: accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult registration(taikhoan_customer model)
        {
            if (ModelState.IsValid)
            {
                var accname = from s in db.accounts
                              where s.accountname == model.accountname
                              select s;
                var checkid = from s in db.accounts
                              where s.id == model.id
                              select s;
                if (accname.Any())
                {
                    ViewBag.message = "Tên tài khoản đã có người dùng";
                }
                else if (checkid.Any())
                {
                    ViewBag.message = "CMND bị trùng";
                }
                else
                {
                    account ac = new account();
                    ac.accountname = model.accountname;
                    //mã hóa mật khẩu
                    MD5 md5 = System.Security.Cryptography.MD5.Create();
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(model.passwords);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("X2"));
                    }
                    ac.passwords = sb.ToString();
                    ac.id = model.id;
                    ac.levels = 2;
                    db.accounts.Add(ac);


                    customer cs = new customer();
                    cs.id = model.id;
                    cs.username = model.username;
                    cs.addres = model.addres;
                    cs.phonenumber = model.phonenumber;
                    cs.sex = model.sex;
                    db.customers.Add(cs);

                    Monney mn = new Monney();
                    mn.id = model.id;
                    mn.monney1 = 0;
                    db.Monneys.Add(mn);

                    db.SaveChanges();

                    ViewBag.message = "Tạo tài khoản thành công";
                    return View();
                }
            }

            
            return View();
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