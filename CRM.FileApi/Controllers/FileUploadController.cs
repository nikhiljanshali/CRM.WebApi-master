using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Configuration;
using CRM.Entities.MongoEntities.FileUpload;
using CRM.DAL.FileUploadRepositories;
using CRM.Logging.Model;
using CRM.Logging.Attributes;
using CRM.Business.FileUpload;

namespace CRM.FileApi.Controllers.FileUpload {
    [Route("api/fileupload")]
    [ApiController]
    public class FileUploadController : ControllerBase {
        private readonly IConfiguration _config;
        private readonly IFileUploadRepository _repository;

        public FileUploadController(IConfiguration config, IFileUploadRepository repository) {
            _config = config;
            _repository = repository;
        }


        [HttpPost("uploadfile", Name = "uploadfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile files, [FromForm] string type, [FromForm] string Ident, [FromForm] string folderName, CancellationToken cancellationToken) {
            try {
                if (_repository.CheckIfExcelFile(files, type)) {
                    var response = await WriteFile(files, Ident, type,folderName);
                    return Ok(response);
                }
                else
                    return BadRequest(new { message = "Invalid file extension" });
            }
            catch (Exception ex) {

                throw ex;
            }

        }      

        private async Task<IActionResult> WriteFile([FromForm] IFormFile file, [FromForm] string Ident, [FromForm] string type, [FromForm] string folderName) {
            bool isSaveSuccess = false;
            string fileName = string.Empty;
            try {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension;
                string baseDrive = _config.GetValue<string>("AppFileApiDirectoryPath");
                string pathBuilt = baseDrive + "\\" + folderName;

                if (!Directory.Exists(pathBuilt)) {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(pathBuilt, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                    await file.CopyToAsync(stream);
                

                var fileStore = new FileStore();
                fileStore.Id = "";
                fileStore.FileName = fileName;
                fileStore.FileExt = extension;
                fileStore.FilePath = path;
                fileStore.FolderName = folderName;
                fileStore.Type = type;
                fileStore.Ident = Ident;
                if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                    fileStore.FileThumbnail = _repository.ResizeImage(path);

                var result = await _repository.InsertFileStoreAsync(fileStore);
                if(result != null) {
                    isSaveSuccess = true;
                    return Ok(new FileUploadResponse { Success = isSaveSuccess, FileName = fileName, Message = "File successfully uploaded." });
                }

                return Ok(new FileUploadResponse { Success = isSaveSuccess, FileName = fileName, Message = "Something went wrong." });

            }
            catch (Exception ex) {
                return Ok(new FileUploadResponse { Success = isSaveSuccess, FileName = "", Message = "Something went wrong." });
                //log error
            }

        }

        [HttpGet]
        [Route("FileStoreByIdent/{ident}")]
        public async Task<IActionResult> GetFileStoreByIdentAsync(string ident) {
            if (string.IsNullOrEmpty(ident))
                return BadRequest("ID is required!");

            var result = await _repository.GetFileStoreByIdentAsync(ident);
            return Ok(result);
        }


        [HttpGet]
        [Route("PathToBase64/{id}/{type}")]
        public async Task<IActionResult> ImagePathToBase64(string id,string type) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID is required!");

            var result = await _repository.GetFileByFileIdAsync(id,type);
             return Ok(result);

        }

        [HttpPut]
        [Route("Update")]
        [DbLogger(LogDataType.Object, "FileStore", "FileStore Updated")]
        public async Task<IActionResult> UpdateFileStoreAsync([FromBody] FileStore fileStore) {
            var result = await _repository.UpdateFileStoreAsync(fileStore);

            return Ok(result);
        }

        [HttpGet]
        [Route("DownloadFile/{fileId}")]
        public async Task<IActionResult> DownloadSingleFile(string fileId) {

            if (string.IsNullOrEmpty(fileId))
                return BadRequest("File Id required");

            bool flag = false;
            flag = await _repository.IsExistFilePath(fileId);
            if(!flag)
                return BadRequest("No File Found");
            
            var result =  await _repository.DownloadFileFromFileId(fileId);
            return result;

        }
    }
}