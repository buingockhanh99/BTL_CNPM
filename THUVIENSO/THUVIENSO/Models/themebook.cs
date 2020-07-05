using System;
using System.Collections.Generic;

namespace THUVIENSO.Models
{
    public partial class themebook
    {
        public int id { get; set; }
        public string nametheme { get; set; }
        public virtual book book { get; set; }
    }
}
