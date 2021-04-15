using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebESchoolData.DataContext;
using WebESchoolData.Model;

namespace WebESchoolData.Repository
{
    public class SchoolRepository : RepositoryBase<School>,ISchoolRepository
    {
        private readonly WebSchoolDataContext _context;

        public SchoolRepository(WebSchoolDataContext context) : base(context)
        {
            _context = context;
        }
    }
}
