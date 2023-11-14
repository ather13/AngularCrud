using CoreCrudApi.Db.Models;

namespace CoreCrudApi.Services.Interfaces
{
    public interface ITenantService
    {
        Task<IEnumerable<Tenant>> Get();
        Task<Tenant> Get(int id);
    }
}