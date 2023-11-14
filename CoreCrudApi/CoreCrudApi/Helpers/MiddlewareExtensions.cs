namespace CoreCrudApi.Helpers
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTenantResolverMiddleware(
            this IApplicationBuilder app)
            => app.UseMiddleware<TenantResolverMiddleware>();
    }
}
