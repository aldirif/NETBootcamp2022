namespace MyBackendApp.Models
{
    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        public List<Battle> Battles { get; set; } = new List<Battle>();
        public Horse Horse { get; set; }
    }
}