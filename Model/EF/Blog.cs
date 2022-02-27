namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Blog
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public long? CategoryBlogID { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool Status { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(500)]
        public string Tag { get; set; }
    }
}
