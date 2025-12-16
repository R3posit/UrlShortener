namespace UrlShorterer.Models
{
    public class UrlModel
    {
        public int Id { get; set; }
        public string? LongUrl { get; set; } = string.Empty;
        public string? ShortUrl { get; set; } = string.Empty;

        public int ClickCount { get; set; } = 0;
    }
}
