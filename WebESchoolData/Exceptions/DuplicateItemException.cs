using System;
using System.Collections.Generic;
using System.Text;

namespace WebESchoolData.Exceptions
{
    public class DuplicateItemException : CustomException
    {
        public DuplicateItemException(string message ="item already exists") : base(message)
        {

        }
    }
}
