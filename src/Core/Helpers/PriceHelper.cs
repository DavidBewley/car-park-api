using System;

namespace Core.Helpers
{
    public static class PriceHelper
    {
        public static decimal CalculatePrice(DateTime startDate, DateTime endDate)
        {
            var price = 0m;

            for (var i = 0; i < (endDate - startDate).Days; i++)
            {
                price += Constants.Prices.PricePerDayOfWeek[startDate.AddDays(i).DayOfWeek];
                price += Constants.Prices.PricePerMonth[startDate.AddDays(i).Month];
            }

            return price;
        }
    }
}
