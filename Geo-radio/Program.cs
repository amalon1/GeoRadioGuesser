using Geo_radio.Components;
using Geo_radio.Services;
using Geo_radio.Models;
using Geo_radio.Services.CountryService;
using Geo_radio.Services.TimerService;
using Geo_radio.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddHttpClient<RadioBrowserService>();
builder.Services.AddScoped<GameState>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TimerService>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<LeaderboardService>();

builder.Services.AddDbContext<LeaderboardDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("LeaderboardDatabase")));

builder.Services.AddScoped<NpgsqlConnection>(service => {
    IConfiguration config = service.GetRequiredService<IConfiguration>();
    String? connectionString = config.GetConnectionString("LeaderboardDatabase");
    return new NpgsqlConnection(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
