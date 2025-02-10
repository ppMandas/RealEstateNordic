using RealEstate.Repository.Enums;

namespace RealEstate.Repository.Entities
{
    public class MunicipalityEntity
    {
        public long Id { get; set; }
        public string Municipality { get; set; }
        public DateOnly DateStart { get; set; }
        public double Tax { get; set; }
        public TaxType TaxType { get; set; }
    }
}
