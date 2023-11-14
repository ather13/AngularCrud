namespace CoreCrudApi.Db.Services
{
    public interface ICurrentTenantService
    {
        string? TenantId { get; set; }

        string? TenantName { get; set; }

        Task<bool> SetTenant(string tenant);
    }
}