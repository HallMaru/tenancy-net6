namespace Tenancy;

/// <summary>
/// Represent a Basic Tenant.
/// </summary>
public class BasicTenant : ITenant
{
    /// <summary>
    /// Indicates if Tenant is Master.
    /// </summary>
    public bool IsMaster { get; private set; }

    /// <summary>
    /// Normalized request hostname.
    /// </summary>
    public string Host { get; private set; }

    /// <summary>
    /// Initialize a new instance of <see cref="BasicTenant"/> class.
    /// </summary>
    /// <param name="isMaster">Indicates if Tenant is Master.</param>
    /// <param name="host">Normalized request hostname.</param>
    public BasicTenant(bool isMaster, string host)
    {
        IsMaster = isMaster;
        Host = host.ToLower();
    }
}
