using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Common {
    public class CommonResponse {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }

        public CommonResponse() { }
    }
}