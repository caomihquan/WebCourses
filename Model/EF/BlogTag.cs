namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BlogTag")]
    public partial class BlogTag
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string TagID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ContentID { get; set; }
    }
}
