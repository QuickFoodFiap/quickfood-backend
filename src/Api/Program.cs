using Microsoft.AspNetCore;

namespace Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("Program");

            try
            {
                logger.LogInformation("Starting application");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.LogInformation("Application stopped by exception");
                throw;
            }
            finally
            {
                logger.LogInformation("Server shutting down");
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Information);
            });
    }
}
