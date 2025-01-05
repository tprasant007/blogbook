using Blazored.LocalStorage;
using Blogs_Client;
using Blogs_Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://blogbookapi.runasp.net/api/") });
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMyBlogService, MyBlogService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddTransient<TokenAuthHandler>();
builder.Services.AddHttpClient<IBlogService, BlogService>(client =>
{
    client.BaseAddress = new Uri("https://blogbookapi.runasp.net/api/");
})
.AddHttpMessageHandler<TokenAuthHandler>();
builder.Services.AddHttpClient<IMyBlogService, MyBlogService>(client =>
{
    client.BaseAddress = new Uri("https://blogbookapi.runasp.net/api/");
})
.AddHttpMessageHandler<TokenAuthHandler>();

var host = builder.Build();

var authenticationService = host.Services.GetRequiredService<IAuthService>();
await authenticationService.Initialize();

await host.RunAsync();
