using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THUVIENSO.Models;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace THUVIENSO.Controllers
{

    [Authorize] //check xem đã đăng nhập chưa
    public class KhachHangController : Controller
    {

        public int _id { get; set; }

        private THUVIENSO_Entities db = new THUVIENSO_Entities();

        public ActionResult Index()
        {
            var books = db.books.Include(b => b.booktopic);
            return View(books.ToList());
        }

        public ActionResult DoiPassword()
        {

            return View();
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

        public ActionResult NapTien()
        {
            ViewBag.id = new SelectList(db.customers, "id", "username");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NapTien(Customer_requirements customer_requirements)
        {
            if (ModelState.IsValid)
            {

                string id1 = Session["id"].ToString();
                int id2 = int.Parse(id1);
                customer_requirements.id = id2;
                customer_requirements.dateyc = DateTime.UtcNow;

                customer_requirements.statuss = 0;
                var check = from s in db.Customer_requirements
                            where s.id == id2
                            select s;


                db.Customer_requirements.Add(customer_requirements);
                db.SaveChanges();
                Response.Write("<script>alert('Gửi yêu cầu nạp tiền thành công')</script>");
                return View();


            }

            ViewBag.id = new SelectList(db.customers, "id", "username", customer_requirements.id);
            return View(customer_requirements);
        }

        public ActionResult ThongTinTaiKhoan()
        {
            string id1 = Session["id"].ToString();
            int id2 = int.Parse(id1);
            var customers = db.customers.Include(c => c.account).Where(c => c.id == id2);
            return View(customers.ToList());
        }


        public ActionResult Edit(int? id)
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
            ViewBag.id = new SelectList(db.accounts, "id", "accountname", customer.id);
            return View(customer);
        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,addres,phonenumber,sex")] customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThongTinTaiKhoan");
            }
            ViewBag.id = new SelectList(db.accounts, "id", "accountname", customer.id);
            return View(customer);
        }







        public ActionResult EditPass()
        {         
  
            return View();
        }

        // POST: accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPass(DoiMK model,string id5, string name_ac)
        {          

            if (ModelState.IsValid)
            {
                
                MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(model.pass_old);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                
                account ac1 = new account();
                ac1.passwords = sb.ToString();
                var checkpass = from s in db.accounts
                                where s.passwords == ac1.passwords
                                && s.accountname == name_ac
                                select s;
                if (checkpass.Any())
                {
                    if (model.pass_new.Equals(model.pass_confim))
                    {
                        MD5 md51 = System.Security.Cryptography.MD5.Create();
                        byte[] inputBytes1 = System.Text.Encoding.ASCII.GetBytes(model.pass_confim);
                        byte[] hashByte1s = md51.ComputeHash(inputBytes1);
                        StringBuilder s1b = new StringBuilder();
                        for (int i = 0; i < hashByte1s.Length; i++)
                        {
                            s1b.Append(hashByte1s[i].ToString("X2"));
                        }
                        int id3 = int.Parse(id5);
                        account ac = db.accounts.Single(s => s.id == id3);
                        ac.passwords = s1b.ToString();


                        db.SaveChanges();

                        ModelState.AddModelError("", "Thay đổi mật khẩu thành công");
                        return RedirectToAction("ThongTinTaiKhoan");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu mới không trùng nhau");
                        return View();
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Mật cũ khẩu không đúng");
                    return View();
                }                             
            }
            return View();

        }
    }
}