namespace MyBackendApp.DTO
{
    public class SamuraiAddWithQuotesDto
    {
        public string Name { get; set; }
        public List<QuoteAddTextDto> Quotes { get; set; }
    }
}