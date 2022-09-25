using KodiaksApi.Core.Finance;
using KodiaksApi.Entity.Finance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KodiaksApi.Areas.Finance
{
    [Area("Finance")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        [HttpGet()]
        [Authorize]
        public async Task<ActionResult> Get(long? id = null)
        {
            try
            {
                var searchResult = await BoMovement.Instance.GetMovement(id);
                return Ok(searchResult);
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
                return Ok(incomeResult);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                if(ex.InnerException != null)
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
                return Ok(incomeResult);
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
                return Ok(incomeResult);

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
