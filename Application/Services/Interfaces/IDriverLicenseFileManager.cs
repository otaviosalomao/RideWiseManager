namespace ride_wise_api.Application.Services.Interfaces
{
    public interface IDriverLicenseFileManager
    {
        Task<string> WriteFile(int number, string base64DriverLicense);
        Task<string> UpdateFile(int number, string base64DriverLicense, string olderFilePath);
    }
}
