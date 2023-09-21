using Core.Helpers;

namespace SpecTests
{
    public class PriceSpec
    {
        [Theory, MemberData(nameof(CalculationData))]
        public void PriceIsCalculatedCorrectly(DateTime startDate, DateTime endDate, decimal expectedResult) 
            => PriceHelper.CalculatePrice(startDate, endDate).Should().Be(expectedResult);


        public static readonly object[][] CalculationData =
        {
            new object[] { new DateTime(2023,1,1), new DateTime(2023,1,2), 5.25m},
            new object[] { new DateTime(2023,6,1), new DateTime(2023,6,2), 5m},
            new object[] { new DateTime(2023,1,2), new DateTime(2023,1,9), 29.25m},
            new object[] { new DateTime(2023,6,5), new DateTime(2023,6,12), 38m},
            new object[] { new DateTime(2023,3,31), new DateTime(2023,4,2), 10.25m},
        };
    }
}
