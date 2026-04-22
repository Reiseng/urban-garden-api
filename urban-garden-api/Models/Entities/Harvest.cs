namespace UrbanGarden.Api.Models.Entities
{
   public class Harvest
    {
        public int Id { get; set; }

        public int PlantedCropId { get; set; }
        public int CropTypeId { get; set; }

        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
    } 
}
