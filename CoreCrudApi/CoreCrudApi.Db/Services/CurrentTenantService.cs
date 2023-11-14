using CoreCrudApi.Db.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CoreCrudApi.Db.Services
{
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly TenantDbContext _tenantDbContext;
        public string? TenantId { get; set; }
        public string? TenantName { get; set; }

        public CurrentTenantService(TenantDbContext tenantDbContext)
        {
            _tenantDbContext = tenantDbContext;
        }
        public async Task<bool> SetTenant(string tenant)
        {
            var tenantInfo = await _tenantDbContext.Tenants.FirstOrDefaultAsync(x => x.TenantName == tenant); // check if tenant exists
            if (tenantInfo != null)
            {
                TenantName = tenant;
                TenantId = tenantInfo.TenantId.ToString();
                return true;
            }
            else
            {
                throw new Exception("Tenant invalid");
            }

        }
    }
}
