namespace Tenancy;

/// <summary>
/// Defines a class that provides mechanisms to resolve tenant of current request.
/// </summary>
public interface ITenantResolverService
{
    /// <summary>
    /// Resolve the Tenant.
    /// </summary>
    /// <returns><see cref="ITenant"/> instance if resolved or null if cannot be resolved.</returns>
    Task<ITenant?> Resolve();
}
