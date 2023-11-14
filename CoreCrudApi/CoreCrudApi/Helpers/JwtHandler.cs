using CoreCrudApi.Db.EntityFramework;
using CoreCrudApi.Db.Models;
using CoreCrudApi.Db.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreCrudApi.Helpers
{
    public class JwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;

        public JwtHandler(IConfiguration configuration
            , EmployeeDBContext employeeDBContext,
ICurrentTenantService? currentTenantService)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }

        public SigningCredentials GetSigningCredentials()
        {
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
            //var key = Encoding.UTF8.GetBytes("AppSecretKey");
            //var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
        }

        public List<Claim> GetClaims(User user, Tenant tenant)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim("tenant", tenant.TenantName));

            // Add roles as multiple claims
            foreach (var role in user?.UserRoleMaps?.Select(x=> x.Role) ?? Enumerable.Empty<Role>())
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }
            
            return claims;
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: "AppAPI",
                audience: "https://localhost:7006",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
