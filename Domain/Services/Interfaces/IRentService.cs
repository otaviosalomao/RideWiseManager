namespace ride_wise_api.Domain.Services.Interfaces
{
    public interface IRentService
    {
        bool ValidPlan(int planNumber);
        decimal RentValue(int daysPlan);        
        DateTime EstimateEndRentDate(DateTime createDate, int daysPlan);
        DateTime StartRentDate(DateTime createDate);
        decimal TotalValueWhenEndBeforeEstimatedDate(DateTime startRentDate, DateTime endRentDate, int daysPlan);
        decimal TotalValueWhenEndAfterEstimatedDate(DateTime estimatedEndRentDate, DateTime endRentDate, int daysPlan);
    }
}
