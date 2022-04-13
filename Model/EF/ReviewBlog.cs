namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReviewBlog")]
    public partial class ReviewBlog
    {
        public long ID { get; set; }

        [Column(TypeName = "ntext")]
        public string Comment { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public long? UserID { get; set; }
        public long? AnswerID { get; set; }

        public long? BlogID { get; set; }

        public bool Status { get; set; }
    }
}
