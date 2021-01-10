using Koledeus.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Koledeus.API.Data
{
    public class KoledeusDbContext : DbContext
    {
        public KoledeusDbContext(DbContextOptions<KoledeusDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClientMetricEntity> ClientMetrics { get; set; }
    }
}