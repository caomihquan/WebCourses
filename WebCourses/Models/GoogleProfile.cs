using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCourses.Models
{
    public class GoogleProfile
    {
        public string Id { get; set; }
        public string email { get; set; }
        public string Name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
    }
}