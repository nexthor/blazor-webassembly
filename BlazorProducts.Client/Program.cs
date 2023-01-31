using BlazorProducts.Client;
using BlazorProducts.Client.HttpRepositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// to show all the logging levels
builder.Logging.SetMinimumLevel(LogLevel.Trace);

builder.Services.AddHttpClient("CompaniesAPI", cl =>
{
    cl.BaseAddress = new Uri("https://localhost:5010/api/");
});

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>()!.CreateClient("CompaniesAPI"));
builder.Services.AddScoped<ICompanyHttpRepository, CompanyHttpRepository>();

await builder.Build().RunAsync();
