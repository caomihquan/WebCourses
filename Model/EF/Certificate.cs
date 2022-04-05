namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Certificate")]
    public partial class Certificate
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public long? IDCourse { get; set; }

        [StringLength(250)]
        public string Logo { get; set; }

        [StringLength(250)]
        public string Image { get; set; }
    }
}
