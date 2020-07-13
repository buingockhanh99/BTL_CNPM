using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace THUVIENSO.Controllers
{
    [Authorize] //check xem đã đăng nhập chưa
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }
    }
}