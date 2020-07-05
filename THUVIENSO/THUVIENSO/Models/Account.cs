using System;
using System.Collections.Generic;

namespace THUVIENSO.Models
{
    public partial class Account
    {
        public string accountname { get; set; }
        public string passwords { get; set; }
        public int id { get; set; }
        public Nullable<int> levels { get; set; }
        public virtual Information Information { get; set; }
        public virtual Monney Monney { get; set; }
    }
}
