namespace RideWise.Api.Application.Models
{
    public class DeliveryAgentFilter
    {
        public DeliveryAgentFilter() { }

        public DeliveryAgentFilter(
            string? identification = null,
            string? name = null,
            string? identificationDocument = null,
            DateTime? birthDate = null,
            int? driverLicenseNumber = null,
            string? driverLicenseType = null,
            string? driverLicenseImage = null)
        {
            Identification = identification;
            Name = name;
            IdentificationDocument = identificationDocument;
            BirthDate = birthDate;
            DriverLicenseNumber = driverLicenseNumber;
            DriverLicenseType = driverLicenseType;
            DriverLicenseImage = driverLicenseImage;
        }

        public string? Identification { get; set; }
        public string? Name { get; set; }
        public string? IdentificationDocument { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? DriverLicenseNumber { get; set; }
        public string? DriverLicenseType { get; set; }
        public string? DriverLicenseImage { get; set; }
    }
}
