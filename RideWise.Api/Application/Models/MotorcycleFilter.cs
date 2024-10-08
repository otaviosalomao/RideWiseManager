namespace RideWise.Api.Application.Models
{
    public class MotorcycleFilter
    {
        public MotorcycleFilter() { }
        public MotorcycleFilter(string? Id = null, int? year = null, string? model = null, string? licensePlate = null)
        {
            this.Id = Id;
            Year = year;
            Model = model;
            LicensePlate = licensePlate;
        }
        public string? Id { get; set; }
        public int? Year { get; set; }
        public string? Model { get; set; }
        public string? LicensePlate { get; set; }
    }
}
