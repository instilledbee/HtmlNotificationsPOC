using HtmlNotificationsPoc.Configuration;
using HtmlNotificationsPoc.Persistence;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// HTTP client factory for the local API endpoints
builder.Services.AddHttpClient("LocalApi", client => client.BaseAddress = new Uri(config.GetValue<string>("ApiUrl")));

builder.Services.AddSingleton<SubscriptionRepository>();

builder.Services.Configure<PushNotificationOptions>(builder.Configuration.GetSection(nameof(PushNotificationOptions)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
