using KodiaksApi.Core.Finance;
using KodiaksApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KodiaksApi.Areas.Finance
{
    [Area("Finance")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class MovementTypeController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get(short? id = null)
        {
            try
            {
                var searchResult = await BoMovementType.Instance.GetMovementType(id);
                var token = Extensions.RefreshLoginToken(User.Claims);
                return Ok(new { token, response = searchResult });
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
