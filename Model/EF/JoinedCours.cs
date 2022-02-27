namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JoinedCourses")]
    public partial class JoinedCours
    {
        public long ID { get; set; }

        public long? CourseID { get; set; }

        public long? UserID { get; set; }

        [StringLength(250)]
        public string CourseName { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public double? Progress { get; set; }

        public bool? Status { get; set; }
    }
}
