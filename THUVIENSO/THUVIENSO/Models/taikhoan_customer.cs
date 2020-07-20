using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace THUVIENSO.Models
{
    public class taikhoan_customer
    {
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string accountname { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string passwords { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số cmnd")]
        public int id { get; set; }
      
        public Nullable<int> levels { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string username { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string addres { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string phonenumber { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public string sex { get; set; }
       
    }
}