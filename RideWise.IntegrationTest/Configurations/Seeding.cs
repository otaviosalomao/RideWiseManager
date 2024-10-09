using Microsoft.EntityFrameworkCore;
using RideWise.Api.Domain.Models;
using RideWise.Api.Infrastructure;

namespace RideWise.IntegrationTest.Configurations
{
    public class Seeding
    {
        public static void FeedDataTestDB(RideWiseApiDbContext db)
        {
            db.DeliveryAgents.ExecuteDelete();
            db.Motorcycles.ExecuteDelete();
            db.Rentals.ExecuteDelete();
            db.DeliveryAgents.AddRange(GetDeliveryAgents());
            db.Motorcycles.AddRange(GetMotorcycles());
            db.Rentals.AddRange(GetRentals());            
            db.SaveChanges();
        }
        private static List<DeliveryAgent> GetDeliveryAgents()
        {
            return new List<DeliveryAgent>()
            {
                new DeliveryAgent() {
                    Id = "1",
                    BirthDate = new DateTime(),
                    DriverLicenseFilePath = "/teste",
                    DriverLicenseNumber = 12345,
                    DriverLicenseType = "A",
                    IdentificationDocument = "1234567893",
                    Name = "Teste",
                },
                 new DeliveryAgent() {
                    Id = "2",
                    BirthDate = new DateTime(),
                    DriverLicenseFilePath = "/teste",
                    DriverLicenseNumber = 123456,
                    DriverLicenseType = "B",
                    IdentificationDocument = "1234567892",
                    Name = "Teste",
                },
                  new DeliveryAgent() {
                    Id = "3",
                    BirthDate = new DateTime(),
                    DriverLicenseFilePath = "/teste",
                    DriverLicenseNumber = 123457,
                    DriverLicenseType = "AB",
                    IdentificationDocument = "1234567891",
                    Name = "Teste",
                },
                new DeliveryAgent() {
                    Id = "4",
                    BirthDate = new DateTime(),
                    DriverLicenseFilePath = "/teste",
                    DriverLicenseNumber = 1234575,
                    DriverLicenseType = "AB",
                    IdentificationDocument = "1234567891",
                    Name = "Teste",
                }
            };
        }
        private static List<Motorcycle> GetMotorcycles()
        {
            return new List<Motorcycle>()
            {
                new Motorcycle() {
                    Id = "1",
                    Model = "Harley",
                   LicensePlate = "GBA-1G95",
                   Year = 2020
                },
                 new Motorcycle() {
                    Id = "2",
                    Model = "CG",
                   LicensePlate = "GBA-1G96",
                   Year = 2022
                },
                  new Motorcycle() {
                   Id = "3",
                    Model = "Susuki",
                   LicensePlate = "GBA-1G97",
                   Year = 2024
                },
                new Motorcycle() {
                   Id = "4",
                    Model = "Yamaha",
                   LicensePlate = "GBA-1G98",
                   Year = 2024
                }
            };
        }
        private static List<Rental> GetRentals()
        {
            var referenceDate = DateTime.UtcNow.Date;
            return new List<Rental>()
            {
                new Rental() {
                    Id = "1",
                    DeliveryAgentIdentification = "1",
                    MotorcycleIdentification = "1",
                    CreatedAt = referenceDate,
                    EstimatedEndDate = referenceDate.AddDays(8),
                    EndDate = referenceDate.AddDays(8),
                    PlanNumber = 7,
                    DailyValue = 30,
                    StartDate = referenceDate.AddDays(1),
                    TotalValue = 100
                },
                 new Rental() {
                    Id = "2",
                    DeliveryAgentIdentification = "2",
                    MotorcycleIdentification = "2",
                    CreatedAt = referenceDate,
                    EstimatedEndDate = referenceDate.AddDays(16),
                    EndDate = referenceDate.AddDays(16),
                    PlanNumber = 15,
                    DailyValue = 28,
                    StartDate = referenceDate.AddDays(1),
                    TotalValue = 100
                },
                 new Rental() {
                    Id = "3",
                    DeliveryAgentIdentification = "3",
                    MotorcycleIdentification = "3",
                    CreatedAt = referenceDate,
                    EstimatedEndDate = referenceDate.AddDays(31),
                    EndDate = referenceDate.AddDays(31),
                    PlanNumber = 30,
                    DailyValue = 22,
                    StartDate = referenceDate.AddDays(1),
                    TotalValue = 100
                }
            };
        }
    }
}
