using NoruBanner.Api.Enums;

namespace NoruBanner.Infrastructure.Entities
{
    public class UserAction
    {
        public Guid Id { get; set; }
        public Guid? BannerId { get; set; }
        public Guid ClientUserId { get; set; }
        public string SourceUrl { get; set; }
        public ActionType ActionType { get; set; }
        public DateTime ActionTime { get; set; }

        public UserAction(Guid? bannerId, Guid clientUserId, string sourceUrl, ActionType actionType)
        {
            BannerId = bannerId;
            ClientUserId = clientUserId;
            SourceUrl = sourceUrl;
            ActionType = actionType;
            ActionTime = DateTime.Now;
        }
    }
}
