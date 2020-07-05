using System;
using System.Collections.Generic;

namespace THUVIENSO.Models
{
    public partial class book
    {
        public int id { get; set; }
        public string namebook { get; set; }
        public string content { get; set; }
        public Nullable<int> price { get; set; }
        public virtual themebook themebook { get; set; }
    }
}
