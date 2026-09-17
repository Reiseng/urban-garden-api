using Microsoft.EntityFrameworkCore;
using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Data
{
    public class UrbanGardenDbContext : DbContext
    {
        public UrbanGardenDbContext(DbContextOptions<UrbanGardenDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GardenPlot>()
                .OwnsOne(g => g.Location);
            modelBuilder.Entity<Device>()
                .OwnsOne(d => d.Config, config =>
                {
                    config.Property(c => c.SoilSensorCount)
                        .HasDefaultValue(5);

                    config.Property(c => c.TemperatureInterval)
                        .HasDefaultValue(900);

                    config.Property(c => c.SoilMoistureInterval)
                        .HasDefaultValue(3600);

                    config.Property(c => c.KeepAliveInterval)
                        .HasDefaultValue(300);
                });
            modelBuilder.Entity<Device>()
                .HasOne(d => d.GardenPlot)
                .WithMany(g => g.Devices)
                .HasForeignKey(d => d.GardenPlotId)
                .IsRequired(false);
        }
        public DbSet<CropType> CropTypes => Set<CropType>();
        public DbSet<GardenPlot> GardenPlots => Set<GardenPlot>();
        public DbSet<PlantedCrop> PlantedCrops => Set<PlantedCrop>();
        public DbSet<Harvest> Harvests => Set<Harvest>();
        public DbSet<Device> Devices => Set<Device>();
        public DbSet<TemperatureSensorReadings> TemperatureSensorReadings => Set<TemperatureSensorReadings>();
        public DbSet<SoilSensorReadings> SoilSensorReadings => Set<SoilSensorReadings>();

    }
}