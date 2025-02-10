using Microsoft.EntityFrameworkCore;
using RealEstate.Repository;
using RealEstate.Repository.Contexts;
using RealEstate.Repository.Interfaces;
using RealEstate.Service;
using RealEstate.Service.Interfaces;
using RealEstate.Service.Mappers;
using RealEstate.Service.Mappings;

namespace RealEstateNordic.Configs
{
    public static class IocConfig
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddControllers();

            // Configure mappings using AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EntityToModelMappings>();
                cfg.AddProfile<ModelToEntityMappings>();
            });

            // Dependency injection
            services.AddDbContext<MunicipalityContext>(opt => opt.UseInMemoryDatabase("MunicipalityDb"));
            services.AddTransient<IMunicipalityRepository, MunicipalityRepository>();
            services.AddTransient<IMunicipalityHandler, MunicipalityHandler>();

            return services;
        }
    }
}
