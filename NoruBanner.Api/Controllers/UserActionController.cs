using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NoruBanner.Api.Models;
using NoruBanner.Api.Services;
using System.Linq;

namespace NoruBanner.Api.Controllers
{
    [ApiController]
    [Route("api/userAction")]
    public class UserActionController : ControllerBase
    {
        private readonly IUserActionService _userActionService;

        public UserActionController(IUserActionService userActionService)
        {
            _userActionService = userActionService;
        }

        /// <summary>
        ///  добавление событий просмотра страницы, просмотра баннера и клика на баннер
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [EnableCors("AllowAllHeaders")]
        public async Task<IActionResult> AddAsync(UserActionForm model)
        {
            await _userActionService.AddAsync(model);
            return Ok();
        }

        /// <summary>
        /// посмотреть количество уникальных посетителей сайта 
        /// (гуид баннера тестового 0fa85f64-5717-4562-b3fc-2c963f66afa6)
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUniqueVisitors/{id}")]
        public async Task<int> GetUniqueVisitors(Guid id)
        {
            var result = await _userActionService.GetUniqueVisitors(id);
            return result;
        }

        /// <summary>
        /// посмотреть количество уникальных пользователей, увидевших баннер на любых сайтах (при желании можно добавить фильтр по домену сайта)
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUniqueBannerViewers/{id}")]
        public async Task<int> GetUniqueBannerViewers(Guid id)
        {
            var result = await _userActionService.GetUniqueBannerViewers(id);
            return result;
        }

        /// <summary>
        /// посмотреть количество уникальных пользователей, кликнувших на баннер (на любых сайтах)  
        /// (гуид баннера тестового 0fa85f64-5717-4562-b3fc-2c963f66afa6)
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUniqueBannerClickers/{id}")]
        public async Task<int> GetUniqueBannerClickers(Guid id)
        {
            var result = await _userActionService.GetUniqueBannerClickers(id);
            return result;
        }

        /// <summary>
        /// получение картинки с баннером
        /// </summary>
        /// <param name="bannerId"></param>
        /// <param name="clientUserId"></param>
        /// <param name="serverUserString"></param>
        /// <returns></returns>
        [HttpGet("img/{bannerId}")]
        public async Task<IActionResult> GetBannerAsync(Guid bannerId, [FromQuery] Guid clientUserId, [FromHeader(Name = "Cookie")]string? serverUserString)
        {
            if (serverUserString == null)
            {
                var avaliableServerUser = _userActionService.CheckCookie(clientUserId);
                serverUserString = avaliableServerUser == null ?  $"serverUserId={Guid.NewGuid()}" : $"serverUserId={avaliableServerUser.ServerUserId}";
                Response.Headers.Append("Set-Cookie", $"{serverUserString}; Max-Age=2628000; path=/; SameSite=None; Secure");
            }
            var result = await _userActionService.GetBannerAsync(bannerId, clientUserId, serverUserString);
            return new FileStreamResult(result.Content, result.ContentType);
        }

        /// <summary>
        /// получение ссылки на страницу рекламодателя по идентификатору баннера
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        [HttpGet("{bannerId}")]
        public async Task<IActionResult> GetBannerUrlAsync(Guid bannerId)
        {
            var result = await _userActionService.GetBannerUrlAsync(bannerId);
            return Ok(result);
        }
    }
}
