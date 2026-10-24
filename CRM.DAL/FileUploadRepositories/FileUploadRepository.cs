using CRM.Business.FileUpload;
using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.FileUpload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CRM.DAL.FileUploadRepositories {
    public class FileUploadRepository  : IFileUploadRepository {
        private readonly IConfiguration _config;

        private readonly IMongoRepository _repository;

        public FileUploadRepository(IConfiguration config,IMongoRepository repository) {
            _config = config;
            _repository = repository;
        }

      

        public async Task<IList<FileStore>> GetFileStoreByIdentAsync(string ident) {
            try {
                return await _repository.GetAllAsync<FileStore>(c => c.Id != null && c.Ident == ident);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<string> GetFileByFileIdAsync(string id, string type) {
            try {
                string base64String = string.Empty;
                var result = await _repository.GetOneAsync<FileStore>(c => c.Id == id && c.Type.ToLower() == type.ToLower());
                if (result != null) {
                    var extension = result.FileExt;
                    if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png") {
                        Image image = Image.FromFile(result.FilePath);
                        using MemoryStream m = new MemoryStream();
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        base64String = Convert.ToBase64String(imageBytes);
                    }
                }

                return base64String;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<string> InsertFileStoreAsync(FileStore fileStore) {
            try {
                await _repository.AddOneAsync(fileStore);

                return fileStore.Id;
            }
            catch (Exception ex) {

                throw ex;
            }

        }

        public async Task<bool> UpdateFileStoreAsync(FileStore fileStore) {
            try {
                var result = await _repository.UpdateOneAsync(fileStore);

                return result;
            }
            catch (Exception ex) {

                throw ex;
            }

        }

        public async Task<bool> IsExistFilePath(string id) {
            try {
                bool flag = false;
                var result = await _repository.GetOneAsync<FileStore>(c => c.Id == id);
                if (result != null && !string.IsNullOrEmpty(result.FilePath))
                    flag = true;
                else
                    flag = false;

                return flag;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<FileStreamResult> DownloadFileFromFileId(string id) {
            try {
                var result = await _repository.GetOneAsync<FileStore>(c => c.Id == id);
                    var stream = File.OpenRead(result.FilePath);
                    return new FileStreamResult(stream, "application/octet-stream") { FileDownloadName = result.FileName };
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public bool CheckIfExcelFile(IFormFile file, string type) {
            try {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                if (type == "profile")
                    return (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png"); // Change the extension based on your need
                else
                    return (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png" || extension.ToLower() == ".pdf"); // Change the extension based on your need

            }
            catch (Exception ex) {

                throw ex;
            }
         }

        public string ResizeImage(string filePath) {
            string thumbnail = string.Empty;
            try {
                using (Image image = Image.FromFile(filePath)) {
                    try {
                        //Size can be change according to your requirement 
                        float thumbWidth = 270F;
                        float thumbHeight = 180F;
                        //calculate  image  size
                        if (image.Width > image.Height) {
                            thumbHeight = ((float)image.Height / image.Width) * thumbWidth;
                        }
                        else {
                            thumbWidth = ((float)image.Width / image.Height) * thumbHeight;
                        }

                        int actualthumbWidth = Convert.ToInt32(Math.Floor(thumbWidth));
                        int actualthumbHeight = Convert.ToInt32(Math.Floor(thumbHeight));
                        var thumbnailBitmap = new Bitmap(actualthumbWidth, actualthumbHeight);
                        var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
                        thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imageRectangle = new Rectangle(0, 0, actualthumbWidth, actualthumbHeight);
                        thumbnailGraph.DrawImage(image, imageRectangle);
                        var ms = new MemoryStream();
                        thumbnailBitmap.Save(ms, ImageFormat.Jpeg);
                        byte[] byteImage = ms.ToArray();
                        thumbnail = Convert.ToBase64String(byteImage);
                        ms.Position = 0;
                        GC.Collect();
                        thumbnailGraph.Dispose();
                        thumbnailBitmap.Dispose();
                        image.Dispose();
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return thumbnail;
        }

        //public async Task<FileUploadResponse> UploadFileAsync(IFormFile file,string Ident, string type, string folderName) {
        //    bool isSaveSuccess = false;
        //    string fileName = string.Empty;
        //    try {
        //        var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        //        fileName = DateTime.Now.Ticks + extension;
        //        string baseDrive = _config.GetValue<string>("AppFileApiDirectoryPath");
        //        //string baseDrive = _config.GetSection("AppFileApiDirectoryPath");


        //        string pathBuilt = baseDrive + "\\" + folderName;

        //        if (!Directory.Exists(pathBuilt)) {
        //            Directory.CreateDirectory(pathBuilt);
        //        }

        //        var path = Path.Combine(pathBuilt, fileName);

        //        using (var stream = new FileStream(path, FileMode.Create))
        //            await file.CopyToAsync(stream);


        //        var fileStore = new FileStore();
        //        fileStore.Id = "";
        //        fileStore.FileName = fileName;
        //        fileStore.FileExt = extension;
        //        fileStore.FilePath = path;
        //        fileStore.FolderName = folderName;
        //        fileStore.Type = type;
        //        fileStore.Ident = Ident;
        //        if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
        //            fileStore.FileThumbnail = this.ResizeImage(path);

        //        var result = await this.InsertFileStoreAsync(fileStore);
        //        if (result != null) {
        //            isSaveSuccess = true;
        //            return new FileUploadResponse { Success = isSaveSuccess, FileName = fileName, Message = "File successfully uploaded." };
        //        }

        //        return new FileUploadResponse { Success = isSaveSuccess, FileName = fileName, Message = "Something went wrong." };

        //    }
        //    catch (Exception ex) {
        //        return new FileUploadResponse { Success = isSaveSuccess, FileName = "", Message = "Something went wrong." };
        //        //log error
        //    }
        //}
    }
}