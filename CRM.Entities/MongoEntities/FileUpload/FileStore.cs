using CRM.Core.MongoCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.MongoEntities.FileUpload {
    public class FileStore: Document {
        public string FolderName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExt { get; set; }
        public string FileThumbnail { get; set; }
        public string Type { get; set; }
        public string Ident { get; set; }
        public string CustomerIdent { get; set; }

        public FileStore() {}
    }
}
