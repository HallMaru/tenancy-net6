using System.Diagnostics.CodeAnalysis;

namespace Tenancy;

/// <summary>
/// Extension methods for adding services to an Microsoft.Extensions.DependencyInjection.IServiceCollection.
/// </summary>
public static class TenancyServiceCollectionExtensions
{
    /// <summary>
    /// Adds Tenancy services to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
    /// Also add a default implementation for the IHttpContextAccessor service.
    /// Can be called multiple times to register multiple services.
    /// </summary>
    /// <typeparam name="ITenantResolverServiceImplementation">The type of the implementation of tenant resolver to use.</typeparam>
    /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add services to.</param>
    public static void AddTenancy<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] ITenantResolverServiceImplementation>(this IServiceCollection services)
        where ITenantResolverServiceImplementation : class, ITenantResolverService
    {
        if (!services.Any(i => i.ServiceType == typeof(IHttpContextAccessor)))
        {
            services.AddHttpContextAccessor();
        }
        services.AddScoped<ITenantResolverService, ITenantResolverServiceImplementation>();
    }
}
