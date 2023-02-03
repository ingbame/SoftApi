using SoftApi.Core;
using SoftApi.Entity.Security;
using SoftApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace SoftApi.Areas.Security
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

                    var response = BoSecurity.Instance.LoginAuthentication(user);

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
        [HttpPost("ChangePassword"), Authorize]
        public async Task<ActionResult> ChangePassword()
        {
            var authReq = Request.Headers["ChangePaswordAuth"].FirstOrDefault();
            if (authReq != null)
            {
                var auth = AuthenticationHeaderValue.Parse(authReq);
                var decodedAuth = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Parameter));
                var usernamePasswordArray = decodedAuth.Split(':');

                LoginEntity user = new LoginEntity
                {
                    UserName = usernamePasswordArray[0],
                    Password = usernamePasswordArray[1]
                };

                var usr = User.Identity as ClaimsIdentity;
                string usrName = "";
                if (usr != null)
                {
                    IEnumerable<Claim> claims = usr.Claims;
                    usrName = claims.Where(w => w.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                    if (user.UserName == usrName && !string.IsNullOrEmpty(user.Password.Trim()))
                    {
                        var response = await BoSecurity.Instance.ChangePassword(user);
                        return Ok(response);
                    }
                    else
                        return BadRequest("Datos incorrectos, verifique su información");
                }
                else
                    return BadRequest("No tiene una sesión activa, no es posible cambiar la contraseña");
            }
            else
                return BadRequest("No se ha enviado correctamente los datos en la cabecera");
        }
    }
}
