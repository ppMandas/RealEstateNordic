using AutoMapper;
using RealEstate.Repository.Entities;
using RealEstate.Service.Models;

namespace RealEstate.Service.Mappings
{
    public class ModelToEntityMappings : Profile
    {
        public ModelToEntityMappings()
        {
            CreateMap<MunicipalityModel, MunicipalityEntity>();
        }
    }
}
