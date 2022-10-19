using KodiaksApi.Core.Application;
using KodiaksApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KodiaksApi.Areas.Application
{
    [Area("Application")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get(short? id = null)
        {
            try
            {
                var searchResult = await BoRole.Instance.GetRole(id);
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
