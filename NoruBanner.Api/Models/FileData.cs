namespace NoruBanner.Api.Models
{
    public class FileData
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }

        public FileData(Stream content, string contentType)
        {
            Content = content;
            ContentType = contentType;
        }
    }
}
