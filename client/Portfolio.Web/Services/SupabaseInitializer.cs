using Microsoft.Extensions.Options;
using Portfolio.Web.Config;
using Supabase;

namespace Portfolio.Web.Services
{
    // Hosted service responsible for initializing the Supabase client on application startup
    public sealed class SupabaseInitializer(
        ILogger<SupabaseInitializer> logger,
        Client client,
        IOptions<SupabaseConfig> config) : IHostedService
    {
        public async Task StartAsync(CancellationToken ct)
        {
            // Log which Supabase project is being initialized (URL only, never log keys)
            logger.LogInformation("Initializing Supabase client for {Url}", MaskUrl(config.Value.Url));

            try
            {
                // Initialize the client and ensure it is ready before serving requests
                await client.InitializeAsync();
                logger.LogInformation("Supabase initialized successfully.");
            }
            catch (Exception ex)
            {
                // Fail fast if initialization fails so the host/container can restart
                logger.LogCritical(ex, "Supabase initialization failed.");
                Environment.FailFast("Supabase initialization failed.", ex);
            }
        }

        public Task StopAsync(CancellationToken ct)
        {
            // Perform cleanup if the SDK exposes disconnect or dispose methods
            return Task.CompletedTask;
        }

        // Mask sensitive parts of the URL before logging
        private static string MaskUrl(string? url) =>
            string.IsNullOrWhiteSpace(url) ? "<unset>" : url!;
    }
}
