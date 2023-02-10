using Blazored.Toast;
using BlazorProducts.Client;
using BlazorProducts.Client.HttpInterceptor;
using BlazorProducts.Client.HttpRepositories;
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
    cl.BaseAddress = new Uri("https://localhost:5010/api/");
    cl.EnableIntercept(sp);
});

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>()!.CreateClient("CompaniesAPI"));
builder.Services.AddScoped<ICompanyHttpRepository, CompanyHttpRepository>();

builder.Services.AddHttpClientInterceptor();
builder.Services.AddScoped<HttpInterceptorService>();

builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
