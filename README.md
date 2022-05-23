<h1>Tenancy for ASP.NET Core 6 &mdash; HallMaru/tenancy-net6<h1>

### *Multi-Tenancy for your ASP.NET Core 6 app.*
### Quickstart
````c#
using Tenancy;

namespace example;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddBasicTenancy(options => // <-- Register Tenancy resolver service.
        {
            options.MasterPathPrefix = "/master/";
            options.SlavePathPrefix = "/tenant/";
            options.MasterHosts = new[] { "master.test" }; // <-- You need to register this hostname in your hosts file.
        });
        var app = builder.Build();
        app.UseTenancy(); // <-- Add Tenancy middleware to pipeline.
        app.UseRouting(); // <-- MUST be added AFTER Tenancy middleware (otherwise internal redirection will not work!)
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/master/", () => "Hello World Master");
            endpoints.MapGet("/tenant/", () => "Hello World Client");
        });
        app.Run();
    }
}

````
### Credits
 - Project create by Felipe José de Oliveira (https://op-code.com)
