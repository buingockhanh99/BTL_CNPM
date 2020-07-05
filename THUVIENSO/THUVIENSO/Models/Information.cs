using System;
using System.Collections.Generic;

namespace THUVIENSO.Models
{
    public partial class Information
    {
        public int id { get; set; }
        public string username { get; set; }
        public virtual Account Account { get; set; }
    }
}
