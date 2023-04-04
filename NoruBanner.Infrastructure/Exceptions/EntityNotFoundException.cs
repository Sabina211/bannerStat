using System.Runtime.Serialization;

namespace NoruBanner.Infrastructure.Exceptions
{
    public class EntityNotFoundException : Exception, ISerializable
    {
        public EntityNotFoundException(string message = "Не удалось найти объект") : base(message)
        {
        }
    }
}
