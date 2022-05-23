namespace Tenancy;

/// <summary>
/// Basic Tenant resolver class that implements ITenantResolverService interface that resolve Tenants based on Request Host and options.
/// </summary>
public class BasicTenantResolverService : HostTenantResolverService
{
    private BasicTenantResolverServiceOptions _options;

    /// <summary>
    /// Construct service with <see cref="HttpContext"/> and <see cref="BasicTenantResolverServiceOptions"/>.
    /// </summary>
    /// <param name="contextAccessor"><see cref="IHttpContextAccessor"/> Service.</param>
    /// <param name="options">Service options.</param>
    public BasicTenantResolverService(IHttpContextAccessor contextAccessor, BasicTenantResolverServiceOptions options) : base(contextAccessor)
    {
        _options = options;
    }

    /// <summary>
    /// Resolve the Tenant base on <see cref="HttpContext"/> Request Host and <see cref="BasicTenantResolverServiceOptions"/>.
    /// </summary>
    /// <param name="host"><see cref="HttpContext"/> Request Host</param>
    /// <returns><see cref="ITenant"/> instance if resolved or null if cannot be resolved.</returns>
    public override async Task<ITenant?> Resolve(HostString host)
    {
        var normalizedHost = host.Host.ToLower();
        if (_options.MasterHosts.Any(i => string.Compare(normalizedHost, i, StringComparison.OrdinalIgnoreCase) == 0))
        {
            if(_options.MasterPathPrefix!= null)
            {
                HttpContext.AddPrefixToRequestPath(_options.MasterPathPrefix.Value);
            }            
            return new BasicTenant(true, normalizedHost);
        }
        if (_options.SlavePathPrefix != null)
        {
            HttpContext.AddPrefixToRequestPath(_options.SlavePathPrefix.Value);
        }
        return new BasicTenant(false, normalizedHost);
    }
}
