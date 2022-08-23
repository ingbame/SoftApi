using KodiaksApi.ApiCommon;
using KodiaksApi.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KodiaksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        [HttpGet("GetMenu"), Authorize]
        public ActionResult GetMenu()
        {
            var currentUser = Session.Instance.GetCurrentUser(HttpContext);
            var menu = BoApplication.Instance.GetMenu(currentUser.User.RoleDescription);
            if (menu.Error)
                return BadRequest(menu.Message);
            return Ok(menu.Model);
        }
        #region Private Methods
        #endregion
    }
}
