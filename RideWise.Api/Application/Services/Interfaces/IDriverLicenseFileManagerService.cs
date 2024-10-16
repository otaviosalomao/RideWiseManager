﻿namespace RideWise.Api.Application.Services.Interfaces
{
    public interface IDriverLicenseFileManagerService
    {
        Task<string> WriteFile(int number, string base64DriverLicense);
        Task<string> UpdateFile(int number, string base64DriverLicense, string olderFilePath);
    }
}
