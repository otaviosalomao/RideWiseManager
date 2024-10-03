﻿namespace ride_wise_api.Application.Services.Interfaces
{
    public interface ILoggerManager
    {
        public void LogInfo(string message);
        public void LogWarn(string message);
        public void LogDebug(string message);
        public void LogError(string message);
    }
}
