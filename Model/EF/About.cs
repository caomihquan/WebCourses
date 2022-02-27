namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("About")]
    public partial class About
    {
        public int ID { get; set; }

        [Column("About", TypeName = "ntext")]
        public string About1 { get; set; }
    }
}
