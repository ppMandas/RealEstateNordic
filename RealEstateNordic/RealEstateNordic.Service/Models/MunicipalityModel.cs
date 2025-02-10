using CsvHelper.Configuration;
using RealEstate.Repository.Enums;
using System.Globalization;

namespace RealEstate.Service.Models
{
    public class MunicipalityModel
    {
        public long Id { get; set; }
        public string Municipality { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public TaxType TaxType { get; set; }
        public double Tax { get; set; }
    }

    public sealed class MunicipalityModelMap : ClassMap<MunicipalityModel>
    {
        public MunicipalityModelMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Id).Ignore();
            Map(m => m.DateEnd).Ignore();
        }
    }
}
