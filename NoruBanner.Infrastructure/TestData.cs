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
                var banners = new List<Banner>() {
                    new Banner(Guid.Parse("0fa85f64-5717-4562-b3fc-2c963f66afa6") , "https://sark.ws", "banner2.PNG", "image/png"),
                    new Banner(Guid.Parse("ff84e3a2-3e0b-460d-b1cc-d414400b4f5d") , "https://tiburon-research.ru/consumer-behavior-monitoring", "banner.PNG", "image/png")
                };
                await context.Banners.AddRangeAsync(banners);
            }
            await context.SaveChangesAsync();
        }
    }
}
