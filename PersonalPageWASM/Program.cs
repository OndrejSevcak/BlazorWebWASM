using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PersonalPageWASM;
using PersonalPageWASM.Extensions;
using PersonalPageWASM.Models;
using PersonalPageWASM.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddCustomServices();

//options pattern
builder.Services.AddOptions<GitHubSettings>().Configure(options =>
{
    options.UserName = "OndrejSevcak";
    options.RepositoryName = "BlazorBlogStorage";
    options.ApiBaseUrl = "https://api.github.com";
});

await builder.Build().RunAsync();
