using CoreCrudApi.Db.EntityFramework;
using CoreCrudApi.Db.Models;
using CoreCrudApi.Helpers;
using CoreCrudApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreCrudApi.Services
{
    public class TenantService : ITenantService
    {
        private readonly EmployeeDBContext _EmployeeDBContext;

        public TenantService(EmployeeDBContext EmployeeDBContext)
        {
            _EmployeeDBContext = EmployeeDBContext;
        }

        public async Task<IEnumerable<Tenant>> Get()
        {
            return await _EmployeeDBContext.Tenants.AsNoTracking().ToListAsync();
        }

        public async Task<Tenant> Get(int id)
        {
            return await _EmployeeDBContext.Tenants.AsNoTracking().FirstOrDefaultAsync(x => x.TenantId == id);
        }

    }
}
