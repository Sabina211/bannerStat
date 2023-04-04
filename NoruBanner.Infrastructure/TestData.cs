using Microsoft.EntityFrameworkCore;
using NoruBanner.Infrastructure.Entities;

namespace NoruBanner.Infrastructure
{
    public class TestData
    {
        public static async Task CreateDataAsync(NoruBannerContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.UserActions.AnyAsync())
            {
                var userAction = new UserAction(Guid.NewGuid(), Guid.NewGuid(), "e1.ru", 0);
                await context.UserActions.AddAsync(userAction);
            }
            if (!await context.Banners.AnyAsync())
            {
                var banner = new Banner(Guid.Parse("0fa85f64-5717-4562-b3fc-2c963f66afa6") , "https://sark.ws", "banner.PNG", "image/png");
                await context.Banners.AddAsync(banner);
            }
            await context.SaveChangesAsync();
        }
    }
}
