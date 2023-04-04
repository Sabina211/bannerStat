using NoruBanner.Infrastructure.Entities;

namespace NoruBanner.Infrastructure.Repositories
{
    public interface IUserActionRepository
    {
        Task AddAsync(UserAction userAction);
        Task<int> GetUniqueVisitors(Guid id);
        Task<int> GetUniqueBannerClickers(Guid id);
        Task<int> GetUniqueBannerViewers(Guid id);
    }
}
