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
    public class ConceptController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get(short? id = null)
        {
            try
            {
                var searchResult = await BoConcept.Instance.GetConcept(id);
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
