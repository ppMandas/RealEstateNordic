using Microsoft.EntityFrameworkCore;
using RealEstate.Repository.Entities;

namespace RealEstate.Repository.Contexts
{
    public class MunicipalityContext : DbContext
    {
        public MunicipalityContext(DbContextOptions<MunicipalityContext> options)
        : base(options)
        {
        }

        public DbSet<MunicipalityEntity> MunicipalityEntities { get; set; }
    }
}
