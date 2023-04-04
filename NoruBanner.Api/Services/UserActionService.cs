using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoruBanner.Api.Models;
using NoruBanner.Infrastructure;
using NoruBanner.Infrastructure.Entities;
using NoruBanner.Infrastructure.Exceptions;
using NoruBanner.Infrastructure.Repositories;
using System.Collections;
using System.IO;

namespace NoruBanner.Api.Services
{
    public class UserActionService : IUserActionService
    {
        private readonly IUserActionRepository _userActionRepository;
        private readonly NoruBannerContext _context;

        public UserActionService(IUserActionRepository userActionRepository, NoruBannerContext context)
        {
            _userActionRepository = userActionRepository;
            _context = context;
        }

        public async Task AddAsync(UserActionForm userAction)
        {
           await _userActionRepository.AddAsync(new Infrastructure.Entities.UserAction(
               userAction.BannerId, 
               userAction.ClientUserId, 
               userAction.SourceUrl,
               userAction.ActionType));
        }

        public async Task<int> GetUniqueBannerClickers(Guid id)
        {
            var result = await _userActionRepository.GetUniqueBannerClickers(id);
            return result;
        }

        public async Task<int> GetUniqueBannerViewers(Guid id)
        {
            var result = await _userActionRepository.GetUniqueBannerViewers(id);
            return result;
        }

        public async Task<int> GetUniqueVisitors(Guid id)
        {
            var result = await _userActionRepository.GetUniqueVisitors(id);
            return result;
        }

        public async Task<FileData> GetBannerAsync(Guid bannerId, Guid clientUserId, string serverUserString)
        {
            var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == bannerId);
            if (banner == null) throw new EntityNotFoundException($"Баннер с Id = {bannerId} не найден");
            await CheckServerUser(clientUserId, serverUserString);
            var file = new MemoryStream(ReadAllBytes(banner.Name));
            return new FileData(file, banner.ContentType);
        }

        public async Task<string> GetBannerUrlAsync(Guid bannerId)
        {
            var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == bannerId);
            if (banner == null) throw new EntityNotFoundException($"Баннер с Id = {bannerId} не найден");
            return banner.SourceUrl;
        }

        private async Task CheckServerUser(Guid clientUserId, string serverUserString)
        {
            var serverUserId = serverUserString.Replace("serverUserId=", "");
            var availableUser = await _context.ServerUsers.FirstOrDefaultAsync(x=>x.ServerUserId==serverUserId && x.ClientUserId==clientUserId);
            if (availableUser != null) return;
            await _context.ServerUsers.AddAsync(new Infrastructure.Entities.ServerUser(clientUserId,  serverUserId));
            await _context.SaveChangesAsync();
        }

        private byte[] ReadAllBytes(string fileName)
        {
            byte[] buffer = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
            }
            return buffer;
        }

        public ServerUser CheckCookie(Guid clientUserId)
        {
           return _context.ServerUsers.FirstOrDefault(x => x.ClientUserId == clientUserId);
        }
    }
}
