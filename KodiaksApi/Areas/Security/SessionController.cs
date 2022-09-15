using KodiaksApi.Core;
using KodiaksApi.Entity.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

namespace KodiaksApi.Areas.Security
{
    [Area("Security")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private IConfiguration _configuration;
        public SessionController(IConfiguration configuration)
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

                    return Ok(new { token = response.Model });
                }
                else
                    return Unauthorized();
            }
            else
                return Unauthorized();
        }
    }
}
