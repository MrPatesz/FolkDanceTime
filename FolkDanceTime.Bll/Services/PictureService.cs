using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FolkDanceTime.Bll.Services
{
    public class PictureService
    {
        private readonly string directory;

        public PictureService(IWebHostEnvironment environment)
        {
            directory = Path.Combine(environment.ContentRootPath, "wwwroot");
        }

        public void DeletePicture(string fileName)
        {
            var toDelete = Path.Combine(directory, fileName);
            if (File.Exists(toDelete))
            {
                File.Delete(toDelete);
            }
        }

        public async Task<string> SavePicture(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception();
            }

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var extension = Path.GetExtension(file.FileName);
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(directory, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(stream);
            }

            return newFileName;
        }
    }
}
