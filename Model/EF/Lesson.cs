namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lesson
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public long? ParentsID { get; set; }

        [StringLength(250)]
        public string Video { get; set; }

        [Column(TypeName = "xml")]
        public string MoreFiles { get; set; }

        public long? CourseID { get; set; }

        [Column(TypeName = "ntext")]
        public string HomeWork { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool Status { get; set; }

        public int? ViewCount { get; set; }
    }
}
