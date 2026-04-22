namespace UrbanGarden.Api.Models.Entities
{
    public class GardenPlot
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public double Size { get; set; }
        public Direction Location { get; set; } = new Direction();
        //public List<Cultive> Cultives { get; set; } = new List<Cultive>();
        //public List<Task> Tasks { get; set; } = new List<Task>();
    }
}