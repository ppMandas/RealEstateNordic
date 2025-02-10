using Microsoft.AspNetCore.Http;
using RealEstate.Service.Models;

namespace RealEstate.Service.Interfaces
{
    public interface IMunicipalityHandler
    {
        int InsertMunicipalityTaxRecord(MunicipalityModel municipalityModel);
        double GetMunicipalityTax(string municipality, DateOnly dateStart);
        void InsertMunicipalityTaxRecords(IFormFile file);
    }
}