using System;
using System.Collections.Generic;
using System.Text;

namespace WebESchoolData.Model
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.CreateDate = DateTime.Now;
            this.ModifiedDate = DateTime.Now;
        }
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

    }
}
