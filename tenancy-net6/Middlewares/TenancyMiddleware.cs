namespace Tenancy;

/// <summary>
/// Tenancy middleware that resolve tenant and set on tenant feature.
/// </summary>
public class TenancyMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Construct Middleware.
    /// </summary>
    /// <param name="next"></param>
    public TenancyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Process request.
    /// </summary>
    /// <param name="context">Current HttpContext.</param>
    /// <param name="tenantResolvers">Registered tenant resolver services.</param>
    public async Task InvokeAsync(HttpContext context, IEnumerable<ITenantResolverService> tenantResolvers)
    {
        ITenant? tenant = null;
        foreach (var tenantResolver in tenantResolvers)
        {
            tenant = await tenantResolver.Resolve();
            if (tenant != null) break;
        }
        context.SetTenant(tenant);
        await _next(context);
    }
}
