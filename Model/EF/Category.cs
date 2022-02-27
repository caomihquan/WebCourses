namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public long? ParentsID { get; set; }

        public int? DisplayOrder { get; set; }

        [Column(TypeName = "ntext")]
        public string Overview { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool Status { get; set; }
    }
}
