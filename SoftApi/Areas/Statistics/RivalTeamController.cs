using SoftApi.Core.Statistics;
using SoftApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftApi.Core.Finance;
using SoftApi.Entity.Finance;
using SoftApi.Entity.Statistics;

namespace SoftApi.Areas.Statistics
{
    [Area("Statistics"), Route("api/[area]/[controller]"), ApiController, Authorize]
    public class RivalTeamController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get(int? id = null)
        {
            try
            {
                var searchResult = await BoRivalTeam.Instance.GetRivalTeam(id);
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
        [HttpPost()]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> Post(RivalTeamEntity request)
        {
            try
            {
                var incomeResult = await BoRivalTeam.Instance.NewRivalTeam(request);
                var token = SoftExtentions.RefreshLoginToken(User.Claims);
                return Ok(new { token, response = incomeResult });
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                if (ex.InnerException != null)
                    message = ex.InnerException.Message;
                return BadRequest(message);
            }
        }
        [HttpPut()]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> Put(long? id, RivalTeamEntity request)
        {
            try
            {
                var incomeResult = await BoRivalTeam.Instance.EditRivalTeam(id, request);
                var token = SoftExtentions.RefreshLoginToken(User.Claims);
                return Ok(new { token, response = incomeResult });
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                if (ex.InnerException != null)
                    message = ex.InnerException.Message;
                return BadRequest(message);
            }
        }
        [HttpDelete()]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Delete(RivalTeamEntity request)
        {
            try
            {
                var incomeResult = await BoRivalTeam.Instance.DeleteRivalTeam(request);
                var token = SoftExtentions.RefreshLoginToken(User.Claims);
                return Ok(new { token, response = incomeResult });

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
