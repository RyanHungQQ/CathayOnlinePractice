using Microsoft.Extensions.Configuration;

namespace Common.Configuration
{
    public static class ConfigurationFactory
    {
        /// <summary>
        /// appsetting設定檔檔名
        /// </summary>
        public static string AppSettingName { get; set; }
        public static IConfigurationRoot ConfigurationRoot { get; set; }

        /// <summary>
        /// 覆寫 production ViewModel DisplayName 設定
        /// </summary>
        public static string? DisplaySettings { get; set; }

        public static void Initial(IConfigurationRoot config)
        {
            ConfigurationRoot = config; ;
        }

        public static IConfigurationRoot Create()
        {
            if (ConfigurationRoot == null)
            {
                var iConfig = new ConfigurationBuilder();
                var config = iConfig.SetBasePath(AppContext.BaseDirectory)
                                    .AddJsonFile(AppSettingName, false, true).Build();
                ConfigurationRoot = config;
            }
            return ConfigurationRoot;
        }
    }
}
