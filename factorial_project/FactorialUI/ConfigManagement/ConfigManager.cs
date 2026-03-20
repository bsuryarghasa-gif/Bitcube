using Microsoft.Extensions.Configuration;

namespace investec_reqnroll_playwright.ConfigManagement
{
    public static class ConfigManager
    {
        private static IConfigurationRoot _configuration;

        static ConfigManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }

        public static string GetSetting(string key)
        {
            return _configuration[key];
        }

        public static T GetSection<T>(string sectionName) where T : new()
        {
            var section = new T();
            _configuration.GetSection(sectionName).Bind(section);
            return section;
        }
    }
}
