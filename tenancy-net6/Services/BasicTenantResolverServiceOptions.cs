namespace Tenancy;

/// <summary>
/// The options to be user by <see cref="BasicTenantResolverService" />.
/// It is not designed to be directly constructed in your application code.
/// </summary>
public class BasicTenantResolverServiceOptions
{
    private IReadOnlyCollection<string> masterHosts = Array.Empty<string>();

    /// <summary>
    /// Normalized Master Hosts Collection. All other hosts will be considerated slaves.
    /// </summary>
    public IReadOnlyCollection<string> MasterHosts { get => masterHosts; set => masterHosts = value.Select(i => i.ToLower()).ToArray(); }

    /// <summary>
    /// PathString to be added to HttpContext Request Path if Tenant is Master.
    /// </summary>
    public PathString? MasterPathPrefix { get; set; } = null;

    /// <summary>
    /// PathString to be added to HttpContext Request Path if Tenant is Slave.
    /// </summary>
    public PathString? SlavePathPrefix { get; set; } = null;
}
