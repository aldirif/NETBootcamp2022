namespace MyBackendApp.DTO
{
    public class SamuraiWithHorseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HorseGetDto Horse { get; set; }
    }
}