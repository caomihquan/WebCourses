namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Cours
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long? CategoryID { get; set; }

        [StringLength(250)]
        public string VideoOverview { get; set; }

        [StringLength(50)]
        public string LevelCourse { get; set; }

        [Column(TypeName = "ntext")]
        public string Overview { get; set; }

        public decimal? Price { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public long? ViewCount { get; set; }

        public bool Status { get; set; }
    }
}
