using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KodiaksApi.Areas.Finance
{
    [Area("Finance")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class BillController : ControllerBase
    {
        [HttpGet("Get")]
        public async Task<ActionResult> Get(long? id = null)
        {
            return Ok();
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Post()
        {
            return Ok();
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Put()
        {
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete()
        {
            return Ok();
        }
    }
}
