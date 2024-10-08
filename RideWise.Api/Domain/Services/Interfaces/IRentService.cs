using RideWise.Api.Domain.Models;

namespace RideWise.Api.Domain.Services.Interfaces
{
    public interface IRentService
    {
        bool ValidPlan(int planNumber);
        decimal RentPlanValue(int daysPlan);
        DateTime EstimateEndRentDate(DateTime createDate, int daysPlan);
        DateTime StartRentDate(DateTime createDate);
        decimal RentTotalValue(Rental rental);        
    }
}
