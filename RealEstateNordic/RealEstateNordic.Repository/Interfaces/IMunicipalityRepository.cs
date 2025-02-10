using RealEstate.Repository.Entities;

namespace RealEstate.Repository.Interfaces
{
    public interface IMunicipalityRepository
    {
        int InsertRecord(MunicipalityEntity municipalityEntity);
        List<MunicipalityEntity> GetRecords(string municipality, DateOnly dateStart);
    }
}