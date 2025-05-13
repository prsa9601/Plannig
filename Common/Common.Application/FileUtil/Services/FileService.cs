using Common.Application.FileUtil.Interfaces;
using Common.Application.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.FileUtil.Services
{
    public class FileService : IFileService
    {
        public void DeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
                Directory.Delete(directoryPath, true);
        }

        public void DeleteFile(string path, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path,
                  fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public string DetermineDirectory(IFormFile file)
        {
            // بررسی امضای فایل (Magic Numbers)
            using var stream = file.OpenReadStream();
            bool isImage = IsImage(stream);
            bool isVideo = IsVideo(stream);

            if (isImage) return "wwwroot/images/Instagram/Story/Images";
            if (isVideo) return "wwwroot/images/Instagram/Story/Videos";

            // اگر امضا تشخیص داده نشد، از پسوند فایل استفاده کنید
            var extension = Path.GetExtension(file.FileName).ToLower();
            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var videoExtensions = new[] { ".mp4", ".avi", ".mov", ".mkv" };

            if (imageExtensions.Contains(extension)) return "wwwroot/images/Instagram/Story/Images";
            if (videoExtensions.Contains(extension)) return "wwwroot/images/Instagram/Story/Videos";

            return null;
        }


        public bool IsImage(Stream stream)
        {
            try
            {
                byte[] header = new byte[8];
                stream.Read(header, 0, header.Length);
                stream.Position = 0; // بازگردانی موقعیت استریم

                // JPEG: FF D8 FF
                if (header.Take(3).SequenceEqual(new byte[] { 0xFF, 0xD8, 0xFF }))
                    return true;

                // PNG: 89 50 4E 47 0D 0A 1A 0A
                if (header.SequenceEqual(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }))
                    return true;

                // GIF: "GIF" در ASCII
                if (header.Take(3).SequenceEqual(new byte[] { 0x47, 0x49, 0x46 }))
                    return true;

                // BMP: "BM" در ASCII
                if (header.Take(2).SequenceEqual(new byte[] { 0x42, 0x4D }))
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool IsVideo(Stream stream)
        {
            try
            {
                byte[] header = new byte[12];
                stream.Read(header, 0, header.Length);
                stream.Position = 0;

                // MP4: فایل‌های MP4 با "ftyp" شروع می‌شوند (از بایت ۴)
                if (header.Skip(4).Take(4).SequenceEqual(new byte[] { 0x66, 0x74, 0x79, 0x70 }))
                    return true;

                // AVI: RIFF فرمت
                if (header.Take(4).SequenceEqual(new byte[] { 0x52, 0x49, 0x46, 0x46 }) &&
                    header.Skip(8).Take(4).SequenceEqual(new byte[] { 0x41, 0x56, 0x49, 0x20 }))
                    return true;

                // MOV: مشابه MP4
                if (header.Skip(4).Take(4).SequenceEqual(new byte[] { 0x66, 0x74, 0x79, 0x70 }))
                    return true;

                // MKV: امضای EBML
                if (header.Take(4).SequenceEqual(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }))
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task SaveFile(IFormFile file, string directoryPath)
        {
            if (file == null)
                throw new InvalidDataException("file is Null");

            var fileName = file.FileName;

            var folderName = Path.Combine(Directory.GetCurrentDirectory(), directoryPath.Replace("/", "\\"));
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            var path = Path.Combine(folderName, fileName);
            using var stream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream);
        }

        public async Task<string> SaveFileAndGenerateName(IFormFile file, string directoryPath)
        {
            if (file == null)
                throw new InvalidDataException("file is Null");

            var fileName = file.FileName;

            fileName = Guid.NewGuid() + DateTime.Now.TimeOfDay.ToString()
                                          .Replace(":", "")
                                          .Replace(".", "") + Path.GetExtension(fileName);

            var folderName = Path.Combine(Directory.GetCurrentDirectory(), directoryPath.Replace("/", "\\"));
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            var path = Path.Combine(folderName, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }
    }
}