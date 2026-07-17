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