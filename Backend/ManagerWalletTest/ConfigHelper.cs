using DotNetEnv;
using Microsoft.Extensions.Configuration; // Tambahkan baris ini

namespace ManagerWalletTest.Helpers
{
    public static class ConfigHelper
    {
        public static void LoadEnvironmentVariables()
        {
            Env.Load();
        }

        public static string GetConnectionString(IConfiguration configuration)
        {
            var template = configuration.GetConnectionString("DefaultConnection");

            return template?
                .Replace("{DbUserId}", Environment.GetEnvironmentVariable("DbUserId"))
                .Replace("{DbPassword}", Environment.GetEnvironmentVariable("DbPassword")) ?? string.Empty;
        }

        public static string GetJwtSecret()
        {
            return Environment.GetEnvironmentVariable("JwtSecretKey") ?? string.Empty;
        }
    }
}