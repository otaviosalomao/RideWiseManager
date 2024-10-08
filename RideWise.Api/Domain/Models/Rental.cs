namespace RideWise.Api.Domain.Models
{
    public class Rental
    {
        public string Identification { get; set; }
        public string DeliveryAgentIdentification { get; set; }
        public string MotorcycleIdentification { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PlanNumber { get; set; }
        public decimal DailyValue { get; set; }
        public decimal TotalValue { get; set; }
    }
}

