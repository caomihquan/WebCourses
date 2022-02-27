namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReviewCourse")]
    public partial class ReviewCourse
    {
        public long ID { get; set; }

        [Column(TypeName = "ntext")]
        public string Comment { get; set; }

        public double? Rating { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public long? UserID { get; set; }

        public long? CourseID { get; set; }

        public bool Status { get; set; }
    }
}
