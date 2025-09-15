using Microsoft.Extensions.Options;
using Supabase;
using Portfolio.Web.Config;
using Portfolio.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Supabase settings from appsettings.json and validate required values
builder.Services.AddOptions<SupabaseConfig>()
    .Bind(builder.Configuration.GetRequiredSection("Supabase"))
    .Validate(cfg => !string.IsNullOrWhiteSpace(cfg.Url), "Supabase:Url is required.")
    .Validate(cfg => !string.IsNullOrWhiteSpace(cfg.AnonKey), "Supabase:AnonKey is required.")
    .ValidateOnStart();

builder.Services.AddRazorPages();
builder.Services.AddHealthChecks();

// Register a single Supabase client instance for dependency injection
builder.Services.AddSingleton<Client>(sp =>
{
    var cfg = sp.GetRequiredService<IOptions<SupabaseConfig>>().Value;

    var options = new SupabaseOptions
    {
        AutoConnectRealtime = false // Connect to realtime only when explicitly required
    };

    return new Client(cfg.Url, cfg.AnonKey, options);
});

// Ensure Supabase is initialized at application startup
builder.Services.AddHostedService<SupabaseInitializer>();

var app = builder.Build();

// Configure error handling: detailed pages in Development, user-friendly in Production
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Map Razor Pages and health check endpoints
app.MapRazorPages().WithStaticAssets();
app.MapHealthChecks("/healthz");

app.Run();
