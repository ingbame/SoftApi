using SoftApi.Data.Security;
using SoftApi.Entity.Common;
using SoftApi.Entity.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SoftApi.Core
{
    public class BoSecurity
    {
        #region Patron de Diseño
        private static BoSecurity _instance;
        private static readonly object _instanceLock = new object();
        public static BoSecurity Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoSecurity();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos
        public ResponseEntity<string> LoginAuthentication(LoginEntity userReq)
        {
            var response = new ResponseEntity<string>();
            try
            {
                var credential = DaSecurity.Instance.GetUser(userReq.UserName);
                if (credential != null)
                {
                    var existingSalt = credential.User.PasswordSalt;
                    var existingPassword = credential.User.Password;
                    var isPasswordMatched = CompareHashedPasswords(userReq.Password, existingPassword, existingSalt);
                    if (isPasswordMatched)
                    {
                        //Genera Token JWT
                        var responseToken = GenerateLoginToken(credential);
                        response.Model = responseToken;
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Credenciales inválidas";
                    }
                }
                else
                {
                    response.Error = true;
                    response.Message = "Credenciales inválidas";
                }
                //response = DaSecurity.Instance.Users();
                return response;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                if (ex.InnerException != null)
                    response.Message += $"\n{ex.InnerException.Message}";
                return response;
            }
        }
        public async Task<ResponseEntity<string>> ChangePassword(LoginEntity userReq)
        {
            var response = new ResponseEntity<string>();
            try
            {
                var credential = DaSecurity.Instance.GetUser(userReq.UserName);
                if (credential != null)
                {
                    string newSalt = GenerateSalt();
                    byte[] hashedPassword = GetHash(userReq.Password, newSalt);
                    string hashedBase64StringPassword = Convert.ToBase64String(hashedPassword);

                    var usrEdit = new UserEntity
                    {
                        UserId = credential.User.UserId,
                        UserName = userReq.UserName,
                        PasswordSalt = newSalt,
                        Password = hashedBase64StringPassword
                    };

                    var isEdited = await DaSecurity.Instance.changePassword(usrEdit);
                    if (isEdited)
                        response.Model = string.Empty;
                    else
                    {
                        response.Error = true;
                        response.Message = "Ocurrió un error al cambiar la contraseña";
                    }
                }
                else
                {
                    response.Error = true;
                    response.Message = "Usuario no encontrado";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                if (ex.InnerException != null)
                    response.Message += $"\n{ex.InnerException.Message}";
                return response;
            }
        }
        #endregion
        #region Metodos privados
        public string GenerateSalt()
        {
            string refreshToken = string.Empty;
            byte[] salt;
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt = new byte[16]);
                refreshToken = Convert.ToBase64String(salt);
            }
            return refreshToken;
        }
        public byte[] GetHash(string PlainPassword, string Salt)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(string.Concat(Salt, PlainPassword));
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] hashedBytes = mySHA256.ComputeHash(byteArray);
                return hashedBytes;
            }
        }
        private bool CompareHashedPasswords(string InputPassword, string ExistingHashedBase64StringPassword, string Salt)
        {
            string inputHashedPassword = Convert.ToBase64String(GetHash(InputPassword, Salt));
            return inputHashedPassword.Equals(ExistingHashedBase64StringPassword);
        }
        private string GenerateLoginToken(CredentialsEntity credential)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var jwtConf = new JwtConfEntity
            {
                Key = configuration["Jwt:Key"],
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConf.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, credential.User.UserName),
                new Claim(ClaimTypes.Name, credential.Member.FullName),
                new Claim(ClaimTypes.GivenName, credential.Member.NickName),
                new Claim(ClaimTypes.Role,credential.User.RoleEn.RoleDescription),
                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject( new { credential.User.CanEdit }))
            };
            var expires = DateTime.Now.AddMinutes(5);
            var token = new JwtSecurityToken(
                jwtConf.Issuer,
                jwtConf.Audience,
                claims,
                expires: expires,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
