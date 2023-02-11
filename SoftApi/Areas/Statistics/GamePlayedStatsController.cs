using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftApi.Core.Statistics;
using SoftApi.Entity.Statistics;
using SoftApi.Util;

namespace SoftApi.Areas.Statistics
{
    [Area("Statistics"), Route("api/[area]/[controller]"), ApiController, Authorize]
    public class GamePlayedStatsController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get(int? id = null)
        {
            try
            {
                var data = new GamePlayedStatsEntity();
                data.Game = BoGamePlayed.Instance.GetGamePlayed(id).Result.FirstOrDefault();
                data.DetailOfOurGame = await BoGamePlayedOurDetail.Instance.GetGamePlayedOurDetailByGame(id);
                data.DetailOfTheRivalGame = await BoGamePlayedRivalDetail.Instance.GetGamePlayedRivalDetailByGame(id);

                var token = SoftExtentions.RefreshLoginToken(User.Claims);
                return Ok(new { token, response = data });
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
