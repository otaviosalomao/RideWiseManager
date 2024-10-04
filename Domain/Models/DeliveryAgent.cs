using ride_wise_api.Domain.Enums;

namespace ride_wise_api.Domain.Models
{
    public class DeliveryAgent
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string IdentificationDocument { get; set; }
        public DateTime BirthDate { get; set; }
        public int DriverLicenseNumber { get; set; }
        public DriverLicenseType DriverLicenseType { get; set; }
        public string DriverLicenseImage { get; set; }
    }
}
