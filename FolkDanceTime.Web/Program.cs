using Client;
using FolkDanceTime.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Tewr.Blazor.FileReader;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("FolkDanceTime.ApiAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("FolkDanceTime.ApiAPI"));

builder.Services.AddApiAuthorization();

builder.Services.AddScoped<ICategoryClient, CategoryClient>();
builder.Services.AddScoped<IItemClient, ItemClient>();
builder.Services.AddScoped<IItemTransactionClient, ItemTransactionClient>();
builder.Services.AddScoped<IUserClient, UserClient>();

builder.Services.AddMudServices();

builder.Services.AddFileReaderService(options => {
    options.UseWasmSharedBuffer = true;
});

await builder.Build().RunAsync();
