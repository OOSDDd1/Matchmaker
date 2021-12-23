using System.IO;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class ConfigService
    {
        public static IConfigurationRoot Get =
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();
    }
}