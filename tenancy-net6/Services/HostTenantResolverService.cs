using Microsoft.AspNetCore.Http.Features;

namespace Tenancy;

/// <summary>
/// Base class that implements <see cref="ITenantResolverService"/> interface that resolve Tenants based on Request Host.
/// </summary>
public abstract class HostTenantResolverService : ITenantResolverService
{
    private readonly HttpContext _context;

    /// <summary>
    /// Current HttpContext.
    /// </summary>
    protected HttpContext HttpContext => _context;

    /// <summary>
    /// Construct service with <see cref="Microsoft.AspNetCore.Http.HttpContext"/>.
    /// </summary>
    /// <param name="contextAccessor">IHttpContextAccessor service.</param>
    /// <exception cref="Exception">Throws exception if Cannot get <see cref="Microsoft.AspNetCore.Http.HttpContext"/> from <see cref="IHttpContextAccessor"/>.</exception>
    public HostTenantResolverService(IHttpContextAccessor contextAccessor)
    {
        _context = contextAccessor.HttpContext ?? throw new Exception("Cannot get HttpContext from IHttpContextAccessor.");
    }

    /// <summary>
    /// Implements base Resolve based on Host Resolve method.
    /// </summary>
    /// <returns><see cref="ITenant"/> instance if resolved or null if cannot be resolved.</returns>
    public async Task<ITenant?> Resolve()
    {
        return await Resolve(_context.Request.Host);
    }

    /// <summary>
    /// Resolve the Tenant base on <see cref="Microsoft.AspNetCore.Http.HttpContext"/> Request Host.
    /// </summary>
    /// <param name="host">HttpContext.Request.Host</param>
    /// <returns><see cref="ITenant"/> instance if resolved or null if cannot be resolved.</returns>
    public abstract Task<ITenant?> Resolve(HostString host);
}
