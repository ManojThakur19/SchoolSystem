using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WebESchoolData
{
    public  class ApiResponse
    {
        [Description("Status code representing states of response")]
        public int Code { get; set; }

        [Description("Response message")]
        public string Message { get; set; }

        [Description("Container of responsed data")]
        public object Data { get; set; }
    }
}
