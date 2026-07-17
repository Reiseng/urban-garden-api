namespace UrbanGarden.Api.Models.Entities
{
   public class Harvest
    {
        public int Id { get; set; }

        public int PlantedCropId { get; set; }
        public PlantedCrop PlantedCrop { get; set; } = null!;
        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
    } 
}
