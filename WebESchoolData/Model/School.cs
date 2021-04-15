using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebESchoolData.Model
{
    public class School: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Address { get; set; }
        public string PanImage { get; set; }
        public string PanNumber { get; set; }
        public string Discription { get; set; }
        public string SchoolType { get; set; }
        public string SchoolLevel { get; set; }

    }
}
