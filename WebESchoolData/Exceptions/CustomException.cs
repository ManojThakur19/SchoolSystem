using System;
using System.Collections.Generic;
using System.Text;

namespace WebESchoolData.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {

        }
    }
}
