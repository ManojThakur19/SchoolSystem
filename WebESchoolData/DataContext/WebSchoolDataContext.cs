using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebESchoolData.Model;

namespace WebESchoolData.DataContext
{
    public class WebSchoolDataContext : DbContext
    {
        public WebSchoolDataContext(DbContextOptions options)
            : base(options)
        {
        }
       public DbSet<School> Schools { get; set; }

    }
}
