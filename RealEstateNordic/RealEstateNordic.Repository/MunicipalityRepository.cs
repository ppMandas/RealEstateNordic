using RealEstate.Repository.Contexts;
using RealEstate.Repository.Entities;
using RealEstate.Repository.Extensions;
using RealEstate.Repository.Interfaces;

namespace RealEstate.Repository
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly MunicipalityContext _context;

        public MunicipalityRepository(MunicipalityContext context)
        {
            _context = context;
        }

        public int InsertRecord(MunicipalityEntity municipalityEntity)
        {
            _context.Add(municipalityEntity);
            return _context.SaveChanges();
        }

        public List<MunicipalityEntity> GetRecords(string municipality, DateOnly dateStart)
        {
            return _context.MunicipalityEntities.Where(
                e => e.Municipality == municipality &&
                dateStart >= e.DateStart &&
                dateStart <= e.DateStart.CalculateDateEnd(e.TaxType)
            ).ToList();
        }
    }
}
