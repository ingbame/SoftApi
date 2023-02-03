using SoftApi.Core.Finance;
using SoftApi.Entity.Finance;
using SoftApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SoftApi.Areas.Finance
{
    [Area("Finance")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class MovementController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get(long? id = null, int? year = null, int? month = null)
        {
            try
            {
                var searchResult = await BoMovement.Instance.GetMovement(id, year, month);
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
        [HttpGet("GetByYearMonth")]
        public async Task<ActionResult> Get(int? year, int? month)
        {
            try
            {
                var searchResult = await BoMovement.Instance.GetMovementByMonthYear(year, month);
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
        [HttpGet("Total")]
        public async Task<ActionResult> GetTotal()
        {
            try
            {
                var searchResult = await BoMovement.Instance.GetTotal();
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
        public async Task<ActionResult> Post(MovementEntity request)
        {
            try
            {
                var incomeResult = await BoMovement.Instance.NewMovement(request);
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
        [HttpPut()]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Put(long? id, MovementEntity request)
        {
            try
            {
                var incomeResult = await BoMovement.Instance.EditMovement(id, request);
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
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Delete(MovementEntity request)
        {
            try
            {
                var incomeResult = await BoMovement.Instance.DeleteMovement(request);
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
