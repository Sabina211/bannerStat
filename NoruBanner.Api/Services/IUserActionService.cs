using Microsoft.AspNetCore.Mvc;
using NoruBanner.Api.Models;
using NoruBanner.Infrastructure.Entities;
using System.Xml.Linq;

namespace NoruBanner.Api.Services
{
    public interface IUserActionService
    {
        Task AddAsync(UserActionForm userAction);
        Task<int> GetUniqueVisitors(Guid id);
        Task<int> GetUniqueBannerClickers(Guid id);
        Task<int> GetUniqueBannerViewers(Guid id);
        Task<FileData> GetBannerAsync(Guid bannerId, Guid clientUserId, string serverUserId);
        Task<string> GetBannerUrlAsync(Guid bannerId);
        ServerUser CheckCookie(Guid clientUserId);
    }
}
