using KodiaksApi.Data;
using KodiaksApi.Entity.Common;
using KodiaksApi.Entity.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KodiaksApi.Core
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
        public ResponseEntity<string> LoginAuthentication(LoginEntity userReq, JwtConfEntity jwtConf)
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
                        var responseToken = GenerateLoginToken(credential, jwtConf);
                        response.Model = responseToken;
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Contraseña incorrecta";
                    }
                }
                else
                {
                    response.Error = true;
                    response.Message = "El usuario no existe";
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

        public ResponseEntity<CredentialsDtoEntity> CreateNewPerson(CredentialsEntity newUser)
        {
            var response = new ResponseEntity<CredentialsDtoEntity>();
            try
            {
                string newSalt = GenerateSalt();
                byte[] hashedPassword = GetHash(newUser.User.Password, newSalt);
                string hashedBase64StringPassword = Convert.ToBase64String(hashedPassword);

                response.Model = DaSecurity.Instance.CreateNewPerson(newUser, newSalt, hashedBase64StringPassword);
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
        private string GenerateSalt()
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
        private byte[] GetHash(string PlainPassword, string Salt)
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
        private string GenerateLoginToken(CredentialsEntity credential, JwtConfEntity jwtConf)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConf.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, credential.User.UserName),
                new Claim(ClaimTypes.Name, credential.Member.FullName),
                new Claim(ClaimTypes.GivenName, credential.Member.NickName),
                new Claim(ClaimTypes.Role,credential.User.Role.RoleDescription)
            };
            var expires = DateTime.Now.AddMinutes(1);
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
