namespace Tenancy;

/// <summary>
/// Interface to be registered in HttpContext features collection.
/// </summary>
public interface ITenantFeature
{
    /// <summary>
    /// Current Tenant.
    /// </summary>
    ITenant? Tenant { get; set; }
}
