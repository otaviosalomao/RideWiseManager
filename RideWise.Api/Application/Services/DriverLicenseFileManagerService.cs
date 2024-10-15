using RideWise.Api.Application.Services.Interfaces;
using RideWise.Common.Services.Interfaces;

namespace RideWise.Api.Application.Services
{
    public class DriverLicenseFileManagerService : IDriverLicenseFileManagerService
    {
        private readonly string BASE_PATH;
        readonly ILoggerManager _logger;

        public DriverLicenseFileManagerService(
            IConfiguration configuration, 
            ILoggerManager logger)
        {
            BASE_PATH = configuration["DriverLicensePath:Path"];
            _logger = logger;
        }

        public async Task<string> UpdateFile(int number, string base64DriverLicense, string olderFilePath)
        {
            _logger.LogInfo($"Updating file from {number}");
            File.Delete(olderFilePath);
            var fullPath = $"{GetFolderDestinationPath}\\{number}";
            return await WriteFile(number, base64DriverLicense);
        }

        public async Task<string> WriteFile(int number, string base64DriverLicense)
        {
            _logger.LogInfo($"Writing file from {number}");
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
