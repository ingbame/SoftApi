using KodiaksApi.Core;
using KodiaksApi.Entity.Application;
using KodiaksApi.Entity.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KodiaksApi.Areas.Application
{
    [Area("Application")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        [HttpPost("CreateNewMember")]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult CreateNewMember(CredentialsEntity credential)
        {
            if (credential == null)
                return NoContent();

            if (string.IsNullOrEmpty(credential.User.UserName?.Trim()))
                return BadRequest("Nombre de usuario vacío.");

            if (string.IsNullOrEmpty(credential.User.Password?.Trim()))
                return BadRequest("Contraseña vacía.");

            if (string.IsNullOrEmpty(credential.Member.FullName?.Trim()))
                return BadRequest("Nombre de persona vacío.");

            if (string.IsNullOrEmpty(credential.Member.NickName?.Trim()))
                credential.Member.NickName = credential.User.UserName;

            if (credential.Member.Birthday == null)
                return BadRequest("Revise su fecha de nacimiento.");

            if (credential.Member.Birthday < DateTime.Parse("1940-01-01"))
                return BadRequest("Excede los 80 años, revise su fecha de nacimiento.");

            if (string.IsNullOrEmpty(credential.Member.CellPhoneNumber?.Trim()))
                return BadRequest("Número de celular vacío.");

            var personaResult = BoSecurity.Instance.CreateNewPerson(credential);
            if (personaResult.Error)
                return BadRequest(personaResult.Message);
            return Ok(personaResult.Model);
        }
        [HttpPost("EditMember")]
        [Authorize]
        public ActionResult EditMember(MemberEntity credential)
        {
            //if (credential == null)
            //    return NoContent();

            //if (string.IsNullOrEmpty(credential.User.UserName?.Trim()))
            //    return BadRequest("Nombre de usuario vacío.");

            //if (string.IsNullOrEmpty(credential.User.Password?.Trim()))
            //    return BadRequest("Contraseña vacía.");

            //if (string.IsNullOrEmpty(credential.Member.FullName?.Trim()))
            //    return BadRequest("Nombre de persona vacío.");

            //if (string.IsNullOrEmpty(credential.Member.NickName?.Trim()))
            //    credential.Member.NickName = credential.User.UserName;

            //if (credential.Member.Birthday == null)
            //    return BadRequest("Revise su fecha de nacimiento.");

            //if (credential.Member.Birthday < DateTime.Parse("1940-01-01"))
            //    return BadRequest("Excede los 80 años, revise su fecha de nacimiento.");

            //if (string.IsNullOrEmpty(credential.Member.CellPhoneNumber?.Trim()))
            //    return BadRequest("Número de celular vacío.");

            //var personaResult = BoSecurity.Instance.CreateNewPerson(credential);
            //if (personaResult.Error)
            //    return BadRequest(personaResult.Message);
            //return Ok(personaResult.Model);
            return NotFound();
        }
    }
}
