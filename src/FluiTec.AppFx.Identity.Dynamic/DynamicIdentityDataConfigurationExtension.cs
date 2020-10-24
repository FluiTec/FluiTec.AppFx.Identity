using System;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Identity.Dapper.Mssql;
using FluiTec.AppFx.Identity.Dapper.Mysql;
using FluiTec.AppFx.Identity.Dapper.Pgsql;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>   A dynamic identity data configuration extension.</summary>
    public static class DynamicIdentityDataConfigurationExtension
    {
        /// <summary>An IServiceCollection extension method that configure dynamic identity data
        /// provider.</summary>
        /// <param name="services">             The services to act on. </param>
        /// <param name="configurationManager"> Manager for configuration. </param>
        /// <returns>   An IServiceCollection.</returns>
        public static IServiceCollection ConfigureDynamicIdentityDataProvider(this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            services.ConfigureDynamicDataProvider(configurationManager,
                new Func<DynamicDataOptions, IServiceProvider, IIdentityDataService>((options, provider) =>
                    {
                        return options.Provider switch
                        {
                            DataProvider.LiteDb => throw new NotImplementedException(),
                            DataProvider.Mssql => new MssqlIdentityDataService(
                                provider.GetRequiredService<MssqlDapperServiceOptions>(),
                                provider.GetService<ILoggerFactory>()),
                            DataProvider.Mysql => new MysqlIdentityDataService(
                                provider.GetRequiredService<MysqlDapperServiceOptions>(),
                                provider.GetService<ILoggerFactory>()),
                            DataProvider.Pgsql => new PgsqlIdentityDataService(
                                provider.GetRequiredService<PgsqlDapperServiceOptions>(),
                                provider.GetService<ILoggerFactory>()),
                            DataProvider.Sqlite => throw new NotImplementedException(),
                            _ => throw new NotImplementedException()
                        };
                    }
                )
            );

            return services;
        }
    }
}