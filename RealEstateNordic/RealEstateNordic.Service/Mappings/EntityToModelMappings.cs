using AutoMapper;
using RealEstate.Repository.Entities;
using RealEstate.Repository.Extensions;
using RealEstate.Service.Models;

namespace RealEstate.Service.Mappers
{
    public class EntityToModelMappings : Profile
    {
        public EntityToModelMappings()
        {
            CreateMap<MunicipalityEntity, MunicipalityModel>()
                .ForMember(
                    dest => dest.DateEnd,
                    memb => memb.MapFrom(src => src.DateStart.CalculateDateEnd(src.TaxType))
                );
        }
    }
}
