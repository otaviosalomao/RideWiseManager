﻿namespace ride_wise_api.Domain.Models
{
    public class Motorcycle
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }        
    }
}

