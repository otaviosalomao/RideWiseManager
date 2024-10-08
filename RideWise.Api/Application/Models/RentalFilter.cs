namespace RideWise.Api.Application.Models
{
    public class RentalFilter
    {
        public RentalFilter() { }
        public RentalFilter(
            string id = null,
            string deliveryAgentIdentification = null,
            string motorcycleIdentification = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            DateTime? estimatedEndDate = null,
            string planNumber = null)
        {
            Id = id;
            DeliveryAgentIdentification = deliveryAgentIdentification;
            MotorcycleIdentification = motorcycleIdentification;
            StartDate = startDate;
            EndDate = endDate;
            EstimatedEndDate = estimatedEndDate;
            PlanNumber = planNumber;
        }

        public string Id { get; set; }
        public string DeliveryAgentIdentification { get; set; }
        public string MotorcycleIdentification { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public string PlanNumber { get; set; }
    }
}
