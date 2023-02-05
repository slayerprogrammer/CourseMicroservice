using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
 {
     //tokenı kimin dağıttığı url bilgisini veriyoruz.
     options.Authority = builder.Configuration.GetSection("IdentityServerUrl").Value;
     options.Audience = "resource_gateway";
     //https kullanmadığımız için alttakini ekliyoruz
     options.RequireHttpsMetadata = false;
 });

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();
});

builder.Services.AddOcelot();

var app = builder.Build();
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();