using Tenancy;

namespace tenancy_net6.sample_01_minimal
{
    public class MyMasterTenant : ITenant
    {

    }
    public class MyClientTenant : ITenant
    {

    }
    public class MyTenantResolver : HostTenantResolverService
    {
        public MyTenantResolver(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            
        }
        // Override this method to create your custom resolver
        public override async Task<ITenant?> Resolve(HostString host)
        {
            if (host.Host == "master.test") // <-- You need to register this hostname in your hosts file.
            {
                HttpContext.Request.Path = new PathString("/master/").Add(HttpContext.Request.Path);
                return new MyMasterTenant();
            }
            if (host.Host == "tenant.test") // <-- You need to register this hostname in your hosts file.
            {
                HttpContext.Request.Path = new PathString("/tenant/").Add(HttpContext.Request.Path);
                return new MyClientTenant();
            }
            HttpContext.Request.Path = new PathString("/unknown/").Add(HttpContext.Request.Path);
            return null;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTenancy<MyTenantResolver>(); // <-- Add your custom middleware tenant resolver.
            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseTenancy(); // <-- Add Tenancy middleware to pipeline.
            app.UseRouting(); // <-- MUST be added AFTER Tenancy middleware (otherwise internal redirection will not work!)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/master/", () =>
                {
                    return "Hello World Master";
                });
                endpoints.MapGet("/tenant/", () =>
                {
                    return "Hello World Client";
                });
                endpoints.MapGet("/unknown/", () =>
                {
                    return "Hello World Unknown";
                });
            });
            app.Run();
        }
    }
}