using Common.Common;
using DAL.DbContexts;
using DAL.Repositories;
using Lib.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DAL
{
    /// <summary>
    /// Adds the DAL Project.
    /// 加入DAL專案的服務注入設定.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static class DependencyInjection
    {
        public static IServiceCollection AddDAL(this IServiceCollection services)
        {
            // 註冊 DbContext 並配置連接字串
            services.AddDbContext<DbEntities>(options =>
                options.UseSqlServer(Setting.ConnectionString));
            // 註冊DbAction
            services.AddScoped<IDbEntities, DbEntities>();

            // 註冊通用型Repository
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
