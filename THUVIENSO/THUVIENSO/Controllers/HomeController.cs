using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace THUVIENSO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult test1()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            //xử lý upload
            file.SaveAs(Server.MapPath("~/images" + file.FileName));
            return "/images/" + file.FileName;
        }
        public ActionResult test2()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult Admin()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize] //check xem đã đăng nhập chưa
        public ActionResult KhachHang()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}