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
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CourseID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserID { get; set; }

        [StringLength(250)]
        public string CourseName { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string LevelCourse { get; set; }

        public long? CategoryID { get; set; }

        public bool? Status { get; set; }
    }
}
