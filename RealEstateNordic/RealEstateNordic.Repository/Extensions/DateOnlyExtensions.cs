using RealEstate.Repository.Enums;

namespace RealEstate.Repository.Extensions
{
    public static class DateOnlyExtensions
    {
        public static DateOnly CalculateDateEnd(this DateOnly dateStart, TaxType taxType)
        {
            if (taxType == TaxType.yearly)
                return dateStart.AddYears(1);

            if (taxType == TaxType.monthly)
                return dateStart.AddMonths(1);

            if (taxType == TaxType.weekly)
                return dateStart.AddDays(7);

            if (taxType == TaxType.daily)
                return dateStart.AddDays(1);

            return default;
        }
    }
}
