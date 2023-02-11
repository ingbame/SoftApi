using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftApi.Core.Statistics;
using SoftApi.Entity.Statistics;
using SoftApi.Util;

namespace SoftApi.Areas.Statistics
{
    [Area("Statistics"), Route("api/[area]/[controller]"), ApiController, Authorize]
    public class GamePlayedOurDetailController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get(int? id = null)
        {
            try
            {
                var searchResult = await BoGamePlayedOurDetail.Instance.GetGamePlayedOurDetail(id);
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
        [HttpGet("GetByGame")]
        public async Task<ActionResult> GetByGame(int? id = null)
        {
            try
            {
                var searchResult = await BoGamePlayedOurDetail.Instance.GetGamePlayedOurDetail(id);
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
        public async Task<ActionResult> Post(GamePlayedOurDetailEntity request)
        {
            try
            {
                var incomeResult = await BoGamePlayedOurDetail.Instance.NewGamePlayedOurDetail(request);
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
        public async Task<ActionResult> Put(long? id, GamePlayedOurDetailEntity request)
        {
            try
            {
                var incomeResult = await BoGamePlayedOurDetail.Instance.EditGamePlayedOurDetail(id, request);
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
        public async Task<ActionResult> Delete(GamePlayedOurDetailEntity request)
        {
            try
            {
                var incomeResult = await BoGamePlayedOurDetail.Instance.DeleteGamePlayedOurDetail(request);
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
