﻿namespace ride_wise_api.Application.Models
{
    public class MotorCycleRequest
    {
        public string Identification { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
    }
}
