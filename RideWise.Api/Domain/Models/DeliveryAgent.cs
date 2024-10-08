namespace RideWise.Api.Domain.Models
{
    public class DeliveryAgent
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string IdentificationDocument { get; set; }
        public DateTime BirthDate { get; set; }
        public int DriverLicenseNumber { get; set; }
        public string DriverLicenseFilePath { get; set; }
        public string DriverLicenseType { get; set; }
    }
}
