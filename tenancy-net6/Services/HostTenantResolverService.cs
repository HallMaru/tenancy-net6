using Microsoft.AspNetCore.Http.Features;

namespace Tenancy;

/// <summary>
/// Base class that implements ITenantResolverService interface that resolve Tenants based on Request Host.
/// </summary>
public abstract class HostTenantResolverService : ITenantResolverService
{
    private readonly HttpContext _context;

    /// <summary>
    /// Current HttpContext.
    /// </summary>
    protected HttpContext HttpContext => _context;

    /// <summary>
    /// Construct service with HttpContext.
    /// </summary>
    /// <param name="contextAccessor">IHttpContextAccessor service.</param>
    /// <exception cref="Exception">Throws exception if Cannot get HttpContext from IHttpContextAccessor.</exception>
    public HostTenantResolverService(IHttpContextAccessor contextAccessor)
    {
        _context = contextAccessor.HttpContext ?? throw new Exception("Cannot get HttpContext from IHttpContextAccessor.");
    }

    /// <summary>
    /// Implements base Resolve based on Host Resolve method.
    /// </summary>
    /// <returns>ITenant instance if resolved or null if cannot be resolved.</returns>
    public async Task<ITenant?> Resolve()
    {
        return await Resolve(_context.Request.Host);
    }

    /// <summary>
    /// Resolve the Tenant base on HttpContext.Request.Host.
    /// </summary>
    /// <param name="host">HttpContext.Request.Host</param>
    /// <returns>ITenant instance if resolved or null if cannot be resolved.</returns>
    public abstract Task<ITenant?> Resolve(HostString host);

    /// <summary>
    /// Add prefix to request path, usually used to redirect endpoints.
    /// </summary>
    /// <param name="prefix">The prefix to be added to request path.</param>
    protected void AddPrefixToRequestPath(PathString prefix)
    {
        _context.Request.Path = prefix.Add(HttpContext.Request.Path);
        var requestFeature = _context.Features.Get<IHttpRequestFeature>();
        if(requestFeature != null)
            requestFeature.RawTarget = _context.Request.Path;
    }
}
