namespace Tenancy;

/// <summary>
/// Extensions class that implements Tenant feature.
/// </summary>
public static class TenancyHttpContextExtensions
{
    private class TenantFeature : ITenantFeature
    {
        public ITenant? Tenant { get; set; }
    }

    /// <summary>
    /// Get current Tenant.
    /// </summary>
    /// <param name="context">Current HttpContext.</param>
    /// <returns>ITenant resolved by registered services or null if not resolved.</returns>
    /// <exception cref="ArgumentNullException">Throws exception if context is null.</exception>
    public static ITenant? GetTenant(this HttpContext context)
    {
        if(context == null) throw new ArgumentNullException("context");
        var feature = context.Features.Get<ITenantFeature>();
        if (feature == null) return null;
        return feature.Tenant;
    }

    /// <summary>
    /// Set current Tenant.
    /// </summary>
    /// <param name="context">Current HttpContext.</param>
    /// <param name="tenancy">Tenant resolved.</param>
    /// <exception cref="ArgumentNullException">Throws exception if context is null.</exception>
    public static void SetTenant(this HttpContext context, ITenant? tenancy)
    {
        if (context == null) throw new ArgumentNullException("context");
        var feature = context.Features.Get<ITenantFeature>();
        if(feature == null)
        {
            if (tenancy == null) return;
            feature = new TenantFeature();
            context.Features.Set<ITenantFeature>(feature);
        }
        feature.Tenant = tenancy;
    }
}
