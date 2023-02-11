using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Util
{
    public static class SoftExtentions
    {
        public static TCopy CopyProperties<TSource, TCopy>(this TSource source, TCopy copy)
        {
            if (source == null || copy == null)
                throw new Exception("Both variables must be instantiated.");
            PropertyInfo[] propSource = source.GetType().GetProperties();
            foreach (PropertyInfo prop in propSource)
            {
                if (copy.GetType().GetProperties().Select(s => s.Name.ToLower()).Contains(prop.Name.ToLower()))
                {
                    var propToCopy = copy.GetType().GetProperties().Where(w => w.Name.ToLower() == prop.Name.ToLower()).FirstOrDefault();
                    if (propToCopy.CanWrite)
                        propToCopy.SetValue(copy, prop.GetValue(source, null), null);
                }
            }
            return copy;
        }
        public static string RefreshLoginToken(IEnumerable<Claim> credential)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var jwtConf = new
            {
                Key = configuration["Jwt:Key"],
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConf.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = credential.ToArray();

            var expires = DateTime.Now.AddMinutes(5);

            var token = new JwtSecurityToken(
                jwtConf.Issuer,
                jwtConf.Audience,
                claims,
                expires: expires,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
