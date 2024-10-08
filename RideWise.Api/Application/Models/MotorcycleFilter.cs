namespace RideWise.Api.Application.Models
{
    public class MotorcycleFilter
    {
        public MotorcycleFilter() { }
        public MotorcycleFilter(string? identification = null, int? year = null, string? model = null, string? licensePlate = null)
        {
            Identification = identification;
            Year = year;
            Model = model;
            LicensePlate = licensePlate;
        }
        public string? Identification { get; set; }
        public int? Year { get; set; }
        public string? Model { get; set; }
        public string? LicensePlate { get; set; }
    }
}
