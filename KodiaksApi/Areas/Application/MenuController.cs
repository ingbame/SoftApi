using KodiaksApi.ApiCommon;
using KodiaksApi.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KodiaksApi.Areas.Application
{
    [Area("Application")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        [HttpGet(), Authorize]
        public ActionResult Get()
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
