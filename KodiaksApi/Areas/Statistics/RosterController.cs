using SoftApi.Core.Finance;
using SoftApi.Core.Statistics;
using SoftApi.Entity.Finance;
using SoftApi.Entity.Statistics;
using SoftApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SoftApi.Areas.Statistics
{
    [Area("Statistics"), Route("api/[area]/[controller]"), ApiController, Authorize]
    public class RosterController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get()
        {
            try
            {
                var searchResult = await BoRoster.Instance.GetRoster();
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
        [HttpPost()]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> Post(RosterEntity request)
        {
            try
            {
                var incomeResult = await BoRoster.Instance.NewRoster(request);
                var token = Extensions.RefreshLoginToken(User.Claims);
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
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> Delete(RosterEntity request)
        {
            try
            {
                var incomeResult = await BoRoster.Instance.DeleteRoster(request);
                var token = Extensions.RefreshLoginToken(User.Claims);
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
