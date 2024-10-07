using RideWise.Api.Application.Services.Interfaces;
using System.IO;

namespace RideWise.Api.Application.Services
{
    public class DriverLicenseFileManagerService : IDriverLicenseFileManagerService
    {
        private readonly string BASE_PATH;

        public DriverLicenseFileManagerService(IConfiguration configuration)
        {
            BASE_PATH = configuration["DriverLicensePath:Path"];
        }

        public async Task<string> UpdateFile(int number, string base64DriverLicense, string olderFilePath)
        {
            File.Delete(olderFilePath);
            var fullPath = $"{GetFolderDestinationPath}\\{number}";
            return await WriteFile(number, base64DriverLicense);
        }

        public async Task<string> WriteFile(int number, string base64DriverLicense)
        {
            var basePath = GetFolderDestinationPath();
            var fullPath = $"{basePath}\\{number}";
            Byte[] bytes = Convert.FromBase64String(base64DriverLicense);
            File.WriteAllBytes(fullPath, bytes);
            return fullPath;
        }

        private string GetFolderDestinationPath()
        {
            var path = $"{BASE_PATH}\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\{DateTime.Now.Day}";
            if (!Directory.Exists(path)) 
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
