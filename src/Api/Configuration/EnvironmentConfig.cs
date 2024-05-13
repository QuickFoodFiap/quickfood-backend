namespace Api.Configuration
{
    public static class EnvironmentConfig
    {
        public static Settings ConfigureEnvironment(IConfiguration configuration)
        {
            var settings = new Settings();
            ConfigurationBinder.Bind(configuration, settings);
            return settings;
        }
    }

    public record Settings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public record ConnectionStrings
    {
        public required string DefaultConnection { get; set; }
    }
}
