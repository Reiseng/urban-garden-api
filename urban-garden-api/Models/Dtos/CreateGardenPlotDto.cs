namespace UrbanGarden.Api.Models.Dtos
{
    public class CreateGardenPlotDto
    {
        public required string Name { get; set; }
        public double Size { get; set; }
        public DirectionDto Location { get; set; } = new DirectionDto();
        //public List<CultiveDto> Cultives { get; set; } = new();
        //public List<TaskDto> Tasks { get; set; } = new();
    }
}