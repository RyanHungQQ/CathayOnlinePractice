using Microsoft.Extensions.DependencyInjection;
using Models.Mappings;

namespace Models
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the models.
        /// 加入Models專案的注入設定.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddModels(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }
    }
}
