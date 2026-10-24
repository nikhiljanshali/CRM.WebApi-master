using CRM.Business.FileUpload;
using CRM.Entities.MongoEntities.FileUpload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.FileUploadRepositories {
    public interface IFileUploadRepository {
        //Task<FileUploadResponse> UploadFileAsync(IFormFile file,string Ident, string type, string folderName);
        Task<string> InsertFileStoreAsync(FileStore fileStore);
        Task<IList<FileStore>> GetFileStoreByIdentAsync(string ident);
        Task<string> GetFileByFileIdAsync(string id,string type);
        Task<bool> UpdateFileStoreAsync(FileStore fileStore);
        bool CheckIfExcelFile(IFormFile file, string type);
        Task<bool> IsExistFilePath(string id);
        Task<FileStreamResult> DownloadFileFromFileId(string id);
        string ResizeImage(string filePath);

    }
}
