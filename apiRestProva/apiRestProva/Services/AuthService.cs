using apiRestProva.Db;
using apiRestProva.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace apiRestProva.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings jwtSettings;
        private readonly ProvaDbContext dbContext;

        public AuthService(IOptions<JwtSettings> jwtSettingsOptions, ProvaDbContext _dbContext)
        {
            jwtSettings = jwtSettingsOptions.Value;
            dbContext = _dbContext;
        }

        public async Task<AuthToken> LoginAsync(LoginRequest request)
        {
            if (request.Password.Equals("pluservice"))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Role, "software developer"),
                    new Claim(ClaimTypes.Email, "mail.diprova@ciao.com")
                    

                };
                
                //prendo chiave simmetrica da file
                var ssKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey));
                //creo firma
                var signingCredentials = new SigningCredentials(ssKey, SecurityAlgorithms.HmacSha256);
                //creoi oggetto token
                var jwtToken = new JwtSecurityToken(
                    issuer: jwtSettings.Issuer,
                    audience: jwtSettings.Audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(60),                
                    signingCredentials: signingCredentials);

                //trasfdormo oggetto in token stringa
                var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                //nuovo oggetto auth response che contiene token
                var result = new AuthToken { AccessToken = accessToken, validity =true };

                await dbContext.Add(result);
                
                return result;
            }
            
            return null;
        }

        public Task<bool> LogoutAsync(AuthToken authToken)
        {
            return dbContext.BurnToken(authToken);

        }

        
    }
}
