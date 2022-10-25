namespace MyBackendApp.DTO
{
    public class SamuraiWithBattleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BattleGetDto> Battles { get; set; }
    }
}