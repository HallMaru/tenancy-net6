namespace Tenancy;

/// <summary>
/// Constains extensions for configuring tenancy on an <see cref="Microsoft.AspNetCore.Builder.IApplicationBuilder"/> .
/// </summary>
public static class TenancyBuilderExtensions
{
    /// <summary>
    /// Adds a <see cref="Tenancy.TenancyMiddleware"/> middleware to the specified <see cref="Microsoft.AspNetCore.Builder.IApplicationBuilder"/>.
    /// Must be called before Routing middlewares.
    /// </summary>
    /// <param name="app">The <see cref="Microsoft.AspNetCore.Builder.IApplicationBuilder"/> to add the middleware to.</param>
    public static void UseTenancy(this IApplicationBuilder app)
    {
        app.UseMiddleware<TenancyMiddleware>();
    }
}
