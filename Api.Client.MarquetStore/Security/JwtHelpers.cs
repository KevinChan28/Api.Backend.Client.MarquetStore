using Api.Client.MarquetStore.Models.Others;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Client.MarquetStore.Security
{
    public static class JwtHelpers
    {

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id",userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddDays(1).ToString("G")),
            };

            if (userAccounts.Rol == 1)
            {
                claims.Add(new Claim(ClaimTypes.Role, DataRoles.ADMINISTRATOR));
            }
            else if (userAccounts.Rol == 2)
            {
                claims.Add(new Claim(ClaimTypes.Role, DataRoles.CUSTOMER));
                claims.Add(new Claim("CustomerOnly", "Customer"));
            }

            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();

            return GetClaims(userAccounts, Id);
        }

        public static UserTokens GenerateToken(UserTokens modeL, JwtSettings jwtSettings)
        {
            try
            {
                UserTokens userToken = new UserTokens();

                if (userToken == null)
                {
                    throw new ArgumentNullException(nameof(userToken));
                }
                //Obtain key secret
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                Guid Id;
                //Expire
                DateTime expireTime = DateTime.Now.AddDays(1);

                //Validty
                userToken.Validty = expireTime.TimeOfDay;

                //GENERATE OUR JWT

                var jwToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(modeL, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials
                    (
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256
                    )
                    );
                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName = modeL.UserName;
                userToken.Id = modeL.Id;
                userToken.GuidId = modeL.GuidId;
                userToken.Rol = modeL.Rol;

                return userToken;
            }
            catch (Exception ex)
            {

                throw new Exception("Error generating thw JWT", ex);
            }
        }
    }
}
