using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.FileUpload {
    public class FileUploadResponse {
        public bool Success { get; set; }
        public string FileName { get; set; }
        public string Message { get; set; }

        public FileUploadResponse() { }
    }
}
