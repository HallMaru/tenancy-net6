using Microsoft.AspNetCore.Http.Features;

namespace Tenancy;

/// <summary>
/// Constains extensions for configuring tenancy on an Microsoft.AspNetCore.Http.HttpContext.
/// </summary>
public static class TenancyHttpContextExtensions
{
    /// <summary>
    /// Change HttpContext Request Path and fix corresponding features.
    /// </summary>
    /// <param name="context">The target HttpContext.</param>
    /// <param name="path">The new request path.</param>
    public static void SetRequestPath(this HttpContext context, PathString path)
    {
        context.Request.Path = path;
        var requestFeature = context.Features.Get<IHttpRequestFeature>();
        if (requestFeature != null)
            requestFeature.RawTarget = context.Request.Path;
    }
    /// <summary>
    /// Add prefix to request path, usually used to redirect endpoints.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="prefix">The prefix to be added to request path.</param>
    public static void AddPrefixToRequestPath(this HttpContext context, PathString prefix)
    {
        SetRequestPath(context, prefix.Add(context.Request.Path));
    }
}
