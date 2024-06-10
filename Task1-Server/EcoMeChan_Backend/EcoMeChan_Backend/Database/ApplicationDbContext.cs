// ApplicationDbContext.cs

using EcoMeChan.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoMeChan.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Consumption> Consumptions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<IoTDevice> IoTDevices { get; set; }
        public DbSet<SensorData> SensorData { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<ConsumptionNorm> ConsumptionNorms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}