using SoftApi.Core.Application;
using SoftApi.Core.Statistics;
using SoftApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SoftApi.Areas.Statistics
{
    [Area("Statistics")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class BattingThrowingSidesController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get(short? id = null)
        {
            try
            {
                var searchResult = await BoBattingThrowingSides.Instance.GetBattingThrowingSides(id);
                var token = Extensions.RefreshLoginToken(User.Claims);
                return Ok(new { token, Response = searchResult });
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                if (ex.InnerException != null)
                    message = ex.InnerException.Message;
                return BadRequest(message);
            }
        }
    }
}
