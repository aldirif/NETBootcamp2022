namespace MyBackendApp.DTO
{
    public class SamuraiWithQuoteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuoteGetDTO> Quotes { get; set; }
    }
}