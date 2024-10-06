using ride_wise_api.Domain.Services.Interfaces;

namespace ride_wise_api.Domain.Services
{
    public class RentService : IRentService
    {
        private readonly Dictionary<int, decimal> RENT_PLAN_VALUES =
            new Dictionary<int, decimal>() {
                { 7,30 }, { 15, 28 }, { 30, 22 }, { 45, 20 }, { 50, 18 }
            };

        public decimal RentValue(int daysPlan)
        {
            if (RENT_PLAN_VALUES.TryGetValue(daysPlan, out var result))
            {
                return result;
            }
            throw new Exception("Invalid rental Plan");
        }
        public DateTime EndRentDate(DateTime startRentDate, int daysPlan)
        {
            return startRentDate.AddDays(daysPlan);
        }

        public decimal TotalValueWhenEndBeforeEstimatedDate(DateTime startRentDate, DateTime endRentDate, int daysPlan)
        {
            if (RENT_PLAN_VALUES.TryGetValue(daysPlan, out var planValue))
            {
                var remainingDays = (endRentDate - startRentDate).Days;
                var dailyValue = planValue / daysPlan;
                var remainingDailiesTotalValue = remainingDays * dailyValue;
                var planFine = 0m;
                switch (daysPlan)
                {
                    case 7:
                        planFine = remainingDailiesTotalValue * 0.2m;
                        break;
                    case 15:
                        planFine = remainingDailiesTotalValue * 0.4m;
                        break;
                }
                return remainingDailiesTotalValue + planFine;
            }
            throw new Exception("Invalid rental Plan");
        }
        public decimal TotalValueWhenEndAfterEstimatedDate(DateTime estimatedEndRentDate, DateTime endRentDate, int daysPlan)
        {
            if (RENT_PLAN_VALUES.TryGetValue(daysPlan, out var planValue))
            {
                var additionalDays = (endRentDate - estimatedEndRentDate).Days;
                var dailyValue = planValue / daysPlan;
                var additionalDailiesTotalValue = (additionalDays * dailyValue);
                var planFine = additionalDays * 50;
                return planFine + additionalDailiesTotalValue;
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
