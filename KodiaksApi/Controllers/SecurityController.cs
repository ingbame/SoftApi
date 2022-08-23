using KodiaksApi.Core;
using KodiaksApi.Entity.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

namespace KodiaksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IConfiguration _configuration;
        public SecurityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("LoginAuthentication"), AllowAnonymous]
        public ActionResult LoginAuthentication()
        {
            var authReq = Request.Headers.Authorization.FirstOrDefault();
            if (authReq != null)
            {
                var auth = AuthenticationHeaderValue.Parse(authReq);
                if (auth != null)
                {
                    var decodedAuth = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Parameter));
                    var usernamePasswordArray = decodedAuth.Split(':');

                    LoginEntity user = new LoginEntity
                    {
                        UserName = usernamePasswordArray[0],
                        Password = usernamePasswordArray[1]
                    };

                    if (string.IsNullOrEmpty(user.UserName))
                        return BadRequest("Nombre de usuario vacío");
                    if (string.IsNullOrEmpty(user.Password))
                        return BadRequest("Contraseña vacía");

                    var jwtConf = new JwtConfEntity
                    {
                        Key = _configuration["Jwt:Key"],
                        Issuer = _configuration["Jwt:Issuer"],
                        Audience = _configuration["Jwt:Audience"]
                    };

                    var response = BoSecurity.Instance.LoginAuthentication(user, jwtConf);

                    if (response.Error)
                        return BadRequest(response.Message);

                    return Ok(response.Model);
                }
                else
                    return Unauthorized();
            }
            else
                return Unauthorized();
        }
        [HttpPost("CreateNewPerson")]
        public ActionResult CreateNewPerson(CredentialsEntity credential)
        {
            if (credential == null)
                return NoContent();

            if (string.IsNullOrEmpty(credential.User.UserName.Trim()))
                return BadRequest("Nombre de usuario vacío.");

            if (string.IsNullOrEmpty(credential.User.Password.Trim()))
                return BadRequest("Contraseña vacía.");

            if (string.IsNullOrEmpty(credential.Member.FullName.Trim()))
                return BadRequest("Nombre de persona vacío.");

            if (string.IsNullOrEmpty(credential.Member.NickName.Trim()))
                credential.Member.NickName = credential.User.UserName;

            if (credential.Member.Birthday == null)
                return BadRequest("Revise su fecha de nacimiento.");

            if (credential.Member.Birthday < DateTime.Parse("1940-01-01"))
                return BadRequest("Excede los 80 años, revise su fecha de nacimiento.");

            if (string.IsNullOrEmpty(credential.Member.CellPhoneNumber.Trim()))
                return BadRequest("Número de celular vacío.");

            var personaResult = BoSecurity.Instance.CreateNewPerson(credential);
            if (personaResult.Error)
                return BadRequest(personaResult.Message);
            return Ok(personaResult.Model);
        }
    }
}
