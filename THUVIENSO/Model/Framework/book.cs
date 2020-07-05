namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("book")]
    public partial class book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(200)]
        public string namebook { get; set; }

        public string content { get; set; }

        public int? price { get; set; }

        public virtual themebook themebook { get; set; }
    }
}
