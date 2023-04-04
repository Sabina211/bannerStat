using System.Runtime.Serialization;

namespace NoruBanner.Infrastructure.Exceptions
{
    public class NoruBannerException : Exception, ISerializable
    {
        public NoruBannerException(string message = "Произошла ошибка") : base(message)
        { }
    }
}
