using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CourseView
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long? CoursesID { get; set; }
        public long? UserID { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }

        public long? CategoryID { get; set; }
        public string LevelCourse { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public long? ViewCount { get; set; }

        public bool Status { get; set; }
    }
}
