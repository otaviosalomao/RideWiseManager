namespace ride_wise_api.Domain.Models
{
    public class Rental
    {
        public string Identification { get; set; }
        public string DeliveryAgentIdentification { get; set; }
        public string MotorcycleIdentification { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string EstimatedEndDate { get; set; }
        public string PlanNumber { get; set; }
        public DeliveryAgent DeliveryAgent { get; set; }
        public Motorcycle Motorcycle { get; set; }
    }
}
