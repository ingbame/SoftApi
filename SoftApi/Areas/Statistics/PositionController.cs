using SoftApi.Core.Statistics;
using SoftApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SoftApi.Areas.Statistics
{
    [Area("Statistics"), Route("api/[area]/[controller]"), ApiController, Authorize]
    public class PositionController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get(short? id = null)
        {
            try
            {
                var searchResult = await BoPosition.Instance.GetPosition(id);
                var token = SoftExtentions.RefreshLoginToken(User.Claims);
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
