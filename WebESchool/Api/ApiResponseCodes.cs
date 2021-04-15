using System;
using System.Collections.Generic;
using System.Text;

namespace WebESchoolData
{
    public class ApiResponseCodes
    {
        public enum Codes
        {
            User_Not_Registerd = 901,
            ModelValidationError = 904,
            LoginError = 902,
            SavingError = 903,
            UserAlreadyExist = 905,
            InvalidFileExtensions = 906,
            FileNotFound = 907,
            InvalidUser = 908,
            Exception = 500,
            Success=200,
            PageNotFound=400
        }
    }
}
