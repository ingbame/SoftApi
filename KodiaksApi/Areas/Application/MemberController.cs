using System;
using KodiaksApi.Core;
using KodiaksApi.Core.Application;
using KodiaksApi.Core.Finance;
using KodiaksApi.Entity.Application;
using KodiaksApi.Entity.Security;
using KodiaksApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KodiaksApi.Areas.Application
{
    [Area("Application")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class MemberController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get(long? id = null)
        {
            try
            {
                var searchResult = await BoMember.Instance.GetMember(id);
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
        public async Task<ActionResult> Post(CredentialsEntity request)
        {
            try
            {
                var personaResult = await BoMember.Instance.CreateMember(request);
                var token = Extensions.RefreshLoginToken(User.Claims);
                return Ok(new { token, response = personaResult });
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
        public async Task<ActionResult> Put(long? id, CredentialsEntity request)
        {
            try
            {
                var result = await BoMember.Instance.EditMember(id, request);
                var token = Extensions.RefreshLoginToken(User.Claims);
                return Ok(new { token, response = result });
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
