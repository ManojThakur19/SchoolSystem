using System;
using System.Collections.Generic;
using System.Text;

namespace WebESchoolData.Exceptions
{
    public class ItemNotFoundException : CustomException
    {
        public ItemNotFoundException(string message="Item doesnot exists") : base(message)
        {

        }
    }
}
