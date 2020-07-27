using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace THUVIENSO.Models
{
    public class DoiMK
    {       
        public string accountname { get; set; }
      
        public string passwords { get; set; }
        public int id { get; set; }
        public Nullable<int> levels { get; set; }

        public virtual customer customer { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu cũ")]
        public string pass_old { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
        public string pass_new { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu mới")]
        public string pass_confim { get; set; }

   
    }
}