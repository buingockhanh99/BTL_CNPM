using System;
using System.Collections.Generic;

namespace THUVIENSO.Models
{
    public partial class Monney
    {
        public int id { get; set; }
        public Nullable<int> monney1 { get; set; }
        public virtual Account Account { get; set; }
    }
}
