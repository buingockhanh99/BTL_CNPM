//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace THUVIENSO.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class customer
    {
        public int id { get; set; }
        public string username { get; set; }
        public string addres { get; set; }
        public string phonenumber { get; set; }
        public string sex { get; set; }
    
        public virtual account account { get; set; }
    }
}
