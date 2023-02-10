using Blazored.Toast;
using BlazorProducts.Client;
using BlazorProducts.Client.Features;
using BlazorProducts.Client.HttpInterceptor;
using BlazorProducts.Client.HttpRepositories;
using BlazorProducts.Entities.Models.Configurations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// to show all the logging levels
builder.Logging.SetMinimumLevel(LogLevel.Trace);

builder.Services.AddHttpClient("CompaniesAPI", (sp, cl) =>
{
    cl.BaseAddress = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"]!);
    cl.EnableIntercept(sp);
});

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>()!.CreateClient("CompaniesAPI"));
builder.Services.AddScoped<ICompanyHttpRepository, CompanyHttpRepository>();

builder.Services.AddHttpClientInterceptor();
builder.Services.AddScoped<HttpInterceptorService>();
builder.Services.Configure<ApiConfiguration>
    (builder.Configuration.GetSection(nameof(ApiConfiguration)));

builder.Services.AddAutoMapper(typeof(MapperProfiles));

builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
