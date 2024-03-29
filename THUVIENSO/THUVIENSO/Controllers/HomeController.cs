﻿using Facebook;
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
using System.Configuration;

namespace THUVIENSO.Controllers
{
    public class HomeController : Controller
    {
         
        private THUVIENSO_Entities db = new THUVIENSO_Entities();
        public ActionResult test()
        {
            
            return View();
        }


        public ActionResult ViewBook1(int? id)
        {

            
            var book = from s in db.books
                       where s.idbook == id
                       select s;
            return View(book.ToList());

          
          
        }


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
                    cs.sodutk = 0;
                    db.customers.Add(cs);

                   

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
                        Response.Write("<script>alert('Vui lòng truy cập trang login dành cho admin')</script>");
                        return View();
                    }
                    else
                    {        
                        foreach (var item in ketqua)
                        {
                            Session["id"] = item.id;   
                            FormsAuthentication.SetAuthCookie(accounts.accountname, RememberMe);                                                     
                            return RedirectToAction("Index", "KhachHang");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
               
            }
            return View(accounts);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["id"] = null;
            return RedirectToAction("Index", "Home");
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings[" FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",


            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code

            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string id = me.id;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                Random _r = new Random();
                int id1 = _r.Next();
                   account ac = new account();
                   ac.accountname = email;
                   ac.passwords = "E10ADC3949BA59ABBE56E057F20F883E";
                   ac.levels = 2;
                   ac.id = id1;
                   db.accounts.Add(ac);

                   customer cs = new customer();
                   cs.id = id1;
                   cs.username = firstname + " " + middlename + " " + lastname;
                   cs.sodutk = 0;
                   db.customers.Add(cs);


                var check_tk = from s in db.accounts
                               where s.accountname == email
                               select s;
                
                if (check_tk.Any())
                {
                    foreach (var item in check_tk)
                    { Session["id"] = item.id; }    
                   
                    FormsAuthentication.SetAuthCookie(ac.accountname, RememberMe);
                    return RedirectToAction("Index", "KhachHang");
                }
                else
                {
                    
                    db.SaveChanges();
                    Session["id"] = id1 ;
                    FormsAuthentication.SetAuthCookie(ac.accountname, RememberMe);
                    return RedirectToAction("Index", "KhachHang");
                }
                    
                    
           
            }
            return View();
        }

    }
}