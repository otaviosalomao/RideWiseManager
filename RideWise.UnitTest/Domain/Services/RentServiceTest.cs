using RideWise.Api.Domain.Models;
using RideWise.Api.Domain.Services;
using Xunit;

namespace RideWise.Test.Domain.Services
{
    public class RentServiceTest
    {
        readonly RentService _sut;
        private readonly Dictionary<int, decimal> RENT_PLAN_VALUES =
            new Dictionary<int, decimal>() {
                { 7,30 }, { 15, 28 }, { 30, 22 }, { 45, 20 }, { 50, 18 }
            };
        private readonly Dictionary<int, decimal> RENT_PLAN_FINE_VALUES =
            new Dictionary<int, decimal>() {
                { 7, 0.2m }, { 15, 0.4m }
            };

        public RentServiceTest()
        {
            _sut = new RentService();
        }

        [Fact]
        public async void RentService_RentValue_Successfull()
        {
            foreach (var item in RENT_PLAN_VALUES)
            {
                var result = _sut.RentPlanValue(item.Key);
                Assert.Equal(item.Value, result);
            }
        }

        [Fact]
        public async void RentService_RentValue_Unsuccessfull()
        {
            Assert.Throws<Exception>(() => _sut.RentPlanValue(5));
        }
        [Fact]
        public async void RentService_EndRentDate_Successfull()
        {
            foreach (var item in RENT_PLAN_VALUES)
            {
                var result = _sut.EndRentDate(DateTime.Now.Date, item.Key);
                Assert.Equal(DateTime.Now.AddDays(item.Key).Date, result);
            }
        }
        [Fact]
        public async void RentService_RentTotalValue_WhenEndBeforeEstimatedDate_Successfull()
        {
            foreach (var item in RENT_PLAN_VALUES)
            {
                var endRentDate = DateTime.Now.AddDays(item.Key - 2).Date;
                var startRentDate = DateTime.Now.Date;
                var estimatedEndRentDate = DateTime.Now.Date.AddDays(item.Key);
                var planNumber = item.Key;
                var planValue = item.Value;
                var rental = new Rental()
                {
                    StartDate = startRentDate,
                    EstimatedEndDate = estimatedEndRentDate,
                    EndDate = endRentDate,
                    PlanNumber = planNumber,
                };
                var result = _sut.RentTotalValue(rental);
                var remainingDays = (startRentDate.AddDays(planNumber) - endRentDate).Days;
                var remainngDaysTotalValue = remainingDays * planValue;
                var usedTotalValue = (endRentDate - startRentDate).Days * planValue;
                var planFine = 0m;
                if (RENT_PLAN_FINE_VALUES.TryGetValue(planNumber, out var fine))
                {
                    planFine = remainngDaysTotalValue * fine;
                }
                var totalValue = usedTotalValue + remainngDaysTotalValue + planFine;
                Assert.Equal(totalValue, result);
            }
        }

        [Fact]
        public async void RentService_RentTotalValue_WhenEndAfterEstimatedDate_Successfull()
        {
            foreach (var item in RENT_PLAN_VALUES)
            {
                var endRentDate = DateTime.Now.AddDays(item.Key + 2).Date;
                var estimatedEndRentDate = DateTime.Now.AddDays(item.Key).Date;
                var startRentDate = DateTime.Now.Date;
                var planValue = item.Value;
                var planNumber = item.Key;
                var rental = new Rental()
                {
                    StartDate = startRentDate,
                    EstimatedEndDate = estimatedEndRentDate,
                    EndDate = endRentDate,
                    PlanNumber = planNumber,
                };
                var result = _sut.RentTotalValue(rental);
                var additionalDays = (endRentDate - estimatedEndRentDate).Days;
                var usedTotalValue = (endRentDate - startRentDate).Days * planValue;
                var additionalDailiesTotalValue = (additionalDays * planValue);
                var planFine = additionalDays * 50;
                var totalValue = usedTotalValue + additionalDailiesTotalValue + planFine;
                Assert.Equal(totalValue, result);
            }
        }
        [Fact]
        public async void RentService_RentTotalValue_Unsuccessfull()
        {
            var rental = new Rental() { PlanNumber = 5 };
            Assert.Throws<Exception>(() => _sut.RentTotalValue(rental));
        }
        [Fact]
        public async void RentService_EstimateEndRentDate_Successfull()
        {
            var result = _sut.EstimateEndRentDate(DateTime.Now.Date, 7);
            Assert.Equal(DateTime.Now.Date.AddDays(7), result);
        }
        [Fact]
        public async void RentService_StartRentDate_Successfull()
        {
            var result = _sut.StartRentDate(DateTime.Now.Date);
            Assert.Equal(DateTime.Now.Date.AddDays(1), result);
        }
    }
}
