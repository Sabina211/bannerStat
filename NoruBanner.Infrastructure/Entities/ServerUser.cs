namespace NoruBanner.Infrastructure.Entities
{
    public class ServerUser
    {
        public Guid Id { get; set; }
        public Guid ClientUserId { get; set; }
        public string? ServerUserId { get; set; }

        public ServerUser() { }
        public ServerUser(Guid clientUserId, string? serverUserId)
        {
            ClientUserId = clientUserId;
            ServerUserId = serverUserId;
        }
    }
}
