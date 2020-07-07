using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace THUVIENSO.Models
{
    public partial class Account
    {
        [Required(ErrorMessage = "Vui Lòng nhập tài khoản")]
        public string accountname { get; set; }
        [Required(ErrorMessage = "Vui Lòng nhập mật khẩu")]
        public string passwords { get; set; }
        public int id { get; set; }
        public Nullable<int> levels { get; set; }
        public virtual Information Information { get; set; }
        public virtual Monney Monney { get; set; }
    }
}
