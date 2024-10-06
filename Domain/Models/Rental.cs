namespace ride_wise_api.Domain.Models
{
    public class Rental
    {        
        public string Identification { get; set; }
        public string DeliveryAgentIdentification { get; set; }
        public string MotorcycleIdentification { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public string PlanNumber { get; set; }        
    }
}
