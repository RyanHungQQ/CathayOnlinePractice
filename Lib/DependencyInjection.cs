using Lib.Services;
using Lib.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Lib
{
    /// <summary>
    /// Adds the Lib Project.
    /// 加入Lib專案的服務注入設定.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static class DependencyInjection
    {
        public static IServiceCollection AddLib(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IBitcoinPriceService, BitcoinPriceService>();
            services.AddScoped<ICurrencyService, CurrencyService>();

            return services;
        }
    }
}
