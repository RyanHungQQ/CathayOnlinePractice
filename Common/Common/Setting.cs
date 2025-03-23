using Common.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Common
{
    /// <summary>
    /// 全域設定檔.
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Gets the connection string.
        /// 取得資料庫連線字串.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public static string ConnectionString
        {
            get
            {
                var setting = new StrongSetting();
                //如果連線帳密需要解密時，可在此進行處理
                return setting.GetConnectionString();
            }
        }

        public static string AESKey
        {
            get
            {
                var setting = new StrongSetting();
                var result = setting.GetAppGeneralSettings<string>("AES:Key");
                return result;
            }
        }

        public static string AESIV
        {
            get
            {
                var setting = new StrongSetting();
                var result = setting.GetAppGeneralSettings<string>("AES:IV");
                return result;
            }
        }
    }

    /// <summary>
    /// 強型別設定檔.
    /// </summary>
    internal class StrongSetting
    {
        private const string DefaultDbConnectionStringName = "DBEntities";

        private readonly IConfigurationRoot _configurationRoot;

        public StrongSetting()
        {
            _configurationRoot = LoadConfigurationRoot();
        }

        public StrongSetting(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot ?? LoadConfigurationRoot();
        }

        /// <summary>
        /// Loads the configuration root.
        /// 讀取設定檔根目錄.
        /// </summary>
        /// <returns></returns>
        private static IConfigurationRoot LoadConfigurationRoot()
        {
            var root = ConfigurationFactory.Create();
            return root;
        }

        /// <summary>
        /// Gets the connection string.
        /// 取得資料庫連線字串.
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return _configurationRoot.GetConnectionString(DefaultDbConnectionStringName);
        }

        /// <summary>
        /// Gets the connection string.
        /// 取得資料庫連線字串.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        /// <returns></returns>
        public string GetConnectionString(string connectionName)
        {
            if (string.IsNullOrWhiteSpace(connectionName))
                connectionName = DefaultDbConnectionStringName;

            return _configurationRoot.GetConnectionString(connectionName);
        }

        /// <summary>
        /// Gets the application general settings.
        /// 取得"應用程式通用設定 AppGeneralSettings"主節點底下的指定名稱設定值.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="settingKeyName">Name of the setting key.</param>
        /// <returns></returns>
        public TType GetAppGeneralSettings<TType>(string settingKeyName)
        {
            return _configurationRoot.GetSection($"AppGeneralSettings:{settingKeyName}").Get<TType>();
        }

    }
}
