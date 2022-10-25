namespace MyBackendApp.DTO
{
    public class QuotesWithSamuraiDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public SamuraiGetDTO Samurai { get; set; }
        public int SamuraiId { get; set; }
    }
}