namespace NoruBanner.Infrastructure.Entities
{
    public class Banner
    {
        public Guid Id { get; set; }
        public string SourceUrl { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }

        public Banner(Guid id, string sourceUrl, string name, string contentType)
        {
            Id = id;
            SourceUrl = sourceUrl;
            Name = name;
            ContentType = contentType;
        }
    }
}
