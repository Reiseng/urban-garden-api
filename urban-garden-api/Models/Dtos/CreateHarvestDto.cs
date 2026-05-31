namespace UrbanGarden.Api.Models.Dtos
{
    public class CreateHarvestDto
    {
        public int cropId { get; set; }
        public decimal Quantity { get; set; }
    }
}