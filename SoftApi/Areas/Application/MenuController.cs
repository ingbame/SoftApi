using SoftApi.ApiCommon;
using SoftApi.Core;
using SoftApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SoftApi.Areas.Application
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
            var token = SoftExtentions.RefreshLoginToken(User.Claims);
            return Ok(new { token, response = menu.Model });
        }
        #region Private Methods
        #endregion
    }
}
