namespace SpecTests.Helpers
{
    public static class DateHelper
    {
        public static DateTime AddDaysToToday(int days)
            => DateTime.Now.AddDays(days).Date;
    }
}
