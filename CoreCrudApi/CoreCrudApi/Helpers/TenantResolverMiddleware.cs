using CoreCrudApi.Db.Models;
using CoreCrudApi.Db.Services;
using System.IdentityModel.Tokens.Jwt;

namespace CoreCrudApi.Helpers
{
    public class TenantResolverMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantResolverMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Get Tenant Id from incoming requests 
        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
            context.Request.Headers.TryGetValue("Authorization", out var tenantFromHeader); // Tenant Id from incoming request header
            if (tenantFromHeader.Any() && !string.IsNullOrEmpty(tenantFromHeader[0]))
            {
                var tenantClaim = new JwtSecurityTokenHandler().ReadJwtToken(tenantFromHeader[0]).Claims.Where(x => x.Type == "tenant")?.FirstOrDefault();

                if (!string.IsNullOrEmpty(tenantClaim?.Value))
                {
                    await currentTenantService.SetTenant(tenantClaim.Value);
                }
            }

            await _next(context);
        }
    }


}
