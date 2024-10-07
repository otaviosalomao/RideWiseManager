using RideWise.Api.Domain.Models;
using RideWise.Api.Domain.Services.Interfaces;

namespace RideWise.Api.Domain.Services
{
    public class RentService : IRentService
    {
        private readonly Dictionary<int, decimal> RENT_PLAN_VALUES =
            new Dictionary<int, decimal>() {
                { 7,30 }, { 15, 28 }, { 30, 22 }, { 45, 20 }, { 50, 18 }
            };
        private readonly Dictionary<int, decimal> RENT_PLAN_FINE_VALUES =
            new Dictionary<int, decimal>() {
                { 7, 0.2m }, { 15, 0.4m }
            };

        public decimal RentPlanValue(int daysPlan)
        {
            if (RENT_PLAN_VALUES.TryGetValue(daysPlan, out var planValue))
            {
                return planValue;
            }
            throw new Exception("Invalid rental Plan");
        }
        public DateTime EndRentDate(DateTime startRentDate, int daysPlan)
        {
            return startRentDate.AddDays(daysPlan);
        }
        public decimal RentTotalValue(Rental rental)
        {
            var totalValue = RentPlanValue(rental.PlanNumber) * rental.PlanNumber;
            if (rental.EndDate > rental.EstimatedEndDate)
            {
                totalValue =
                    TotalValueWhenEndAfterEstimatedDate(rental);
            }
            if (rental.EndDate < rental.EstimatedEndDate)
            {
                totalValue =
                    TotalValueWhenEndBeforeEstimatedDate(rental);
            }
            return totalValue;
        }
        private decimal TotalValueWhenEndBeforeEstimatedDate(Rental rental)
        {
            var planNumber = rental.PlanNumber;
            var startRentDate = rental.StartDate;
            if (RENT_PLAN_VALUES.TryGetValue(rental.PlanNumber, out var planValue))
            {
                var remainingDays = (startRentDate.AddDays(planNumber) - rental.EndDate).Days;
                var remainingDailiesTotalValue = remainingDays * planValue;
                var usedTotalValue = (rental.EndDate - startRentDate).Days * planValue;
                var planFine = 0m;
                if (RENT_PLAN_FINE_VALUES.TryGetValue(planNumber, out var fine))
                {
                    planFine = remainingDailiesTotalValue * fine;
                }
                return usedTotalValue + remainingDailiesTotalValue + planFine;
            }
            throw new Exception("Invalid rental Plan");
        }
        private decimal TotalValueWhenEndAfterEstimatedDate(Rental rental)
        {
            var planNumber = rental.PlanNumber;
            var startRentDate = rental.StartDate;
            var estimatedEndRentDate = rental.EstimatedEndDate;
            if (RENT_PLAN_VALUES.TryGetValue(planNumber, out var planValue))
            {
                var additionalDays = (rental.EndDate - estimatedEndRentDate).Days;
                var usedTotalValue = (rental.EndDate - startRentDate).Days * planValue;
                var additionalDailiesTotalValue = (additionalDays * planValue);
                var planFine = additionalDays * 50;
                return usedTotalValue + planFine + additionalDailiesTotalValue;
            }
            throw new Exception("Invalid rental Plan");
        }

        public DateTime EstimateEndRentDate(DateTime startRentDate, int daysPlan)
        {
            return startRentDate.AddDays(daysPlan);
        }

        public DateTime StartRentDate(DateTime createDate)
        {
            return createDate.AddDays(1);
        }

        public bool ValidPlan(int planNumber)
        {
            return RENT_PLAN_VALUES.TryGetValue(planNumber, out var planValue);
        }
    }
}
