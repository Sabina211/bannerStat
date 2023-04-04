using NoruBanner.Api.Enums;

namespace NoruBanner.Api.Models
{
    public class UserActionForm
    {
        public Guid? BannerId { get; set; }
        public Guid ClientUserId { get; set; }
        public string SourceUrl { get; set; }
        public ActionType ActionType { get; set; }
    }
}
