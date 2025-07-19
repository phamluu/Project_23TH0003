using Microsoft.AspNetCore.Hosting;

namespace QLHocPhan_23TH0003.Service
{
    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> UploadAndGetResultStringAsync(IFormFile file, string oldFileName = null)
        {
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                // remove file cũ
                DeleteFile(oldFileName);
                return fileName;
            }
            return null;
        }

        public bool DeleteFile(string fileName)
        {
            if (fileName == null) return false;
            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
            var filePath = Path.Combine(uploads, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
