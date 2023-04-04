using Microsoft.EntityFrameworkCore;
using NoruBanner.Api.Enums;
using NoruBanner.Infrastructure.Entities;

namespace NoruBanner.Infrastructure.Repositories
{
    public class UserActionRepository : IUserActionRepository
    {
        private readonly NoruBannerContext _context;

        public UserActionRepository(NoruBannerContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserAction userAction)
        {
            await _context.UserActions.AddAsync(userAction);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUniqueVisitors(Guid id)
        {
            return await GetBannerStatisticByAction(id, ActionType.OpenSite);
        }

        public async Task<int> GetUniqueBannerViewers(Guid id)
        {
            return await GetBannerStatisticByAction(id, ActionType.ViewBanner);
        }

        public async Task<int> GetUniqueBannerClickers(Guid id)
        {
            return await GetBannerStatisticByAction(id, ActionType.ClickBanner);
        }

        private async Task<int> GetBannerStatisticByAction(Guid id, ActionType actionType)
        {
            return await (from clients in (_context.UserActions
                .Where(x => x.ActionType == actionType && x.BannerId == id)
                .Select(x => x.ClientUserId)
                .Distinct())
                          join serverUsers in _context.ServerUsers
                          on clients equals serverUsers.ClientUserId
                          select serverUsers.ServerUserId).Distinct().CountAsync();
        }
    }
}
