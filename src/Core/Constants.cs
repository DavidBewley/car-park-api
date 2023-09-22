using System;
using System.Collections.Generic;

namespace Core
{
    public static class Constants
    {
        public static class Prices
        {
            public static IReadOnlyDictionary<DayOfWeek, decimal> PricePerDayOfWeek = new Dictionary<DayOfWeek, decimal>()
            {
                {DayOfWeek.Monday,2.50m},
                {DayOfWeek.Tuesday,2.50m},
                {DayOfWeek.Wednesday,2.50m},
                {DayOfWeek.Thursday,2.50m},
                {DayOfWeek.Friday,2.50m},
                {DayOfWeek.Saturday,4.00m},
                {DayOfWeek.Sunday,4.00m},
            };

            public static IReadOnlyDictionary<int, decimal> PricePerMonth = new Dictionary<int, decimal>()
            {
                {1,1.25m},
                {2,1.25m},
                {3,1.25m},
                {4,2.50m},
                {5,2.50m},
                {6,2.50m},
                {7,2.50m},
                {8,2.50m},
                {9,2.50m},
                {10,1.25m},
                {11,1.25m},
                {12,1.25m},
            };
        }

        public static class Messages
        {
            public const string NoSpacesAvailable = "No spaces are available for the dates provided";
            public const string BookingNotFound = "No booking with that reference could be found";
        }
    }
}
