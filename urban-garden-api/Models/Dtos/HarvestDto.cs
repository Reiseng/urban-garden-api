namespace UrbanGarden.Api.Models.Dtos
{
       public class HarvestDto
    {
        public int PlantedCropId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
    } 
}