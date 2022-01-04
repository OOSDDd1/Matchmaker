using System.IO;
using Microsoft.Extensions.Configuration;

namespace Services
{
    /**
     * Here are all the secrets stored. They are loaded from the `appsettings.json` file.
     */
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