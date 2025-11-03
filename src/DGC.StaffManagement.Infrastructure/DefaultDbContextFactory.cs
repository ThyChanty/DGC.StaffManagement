using DGC.Staff_Management.Share.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;


namespace DGC.Staff_Management.Infrastructure
{
    public class DefaultDbContextFactory : IDefaultDbContextFactory, IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly string? _connectionString;

        public DefaultDbContextFactory()
        {
            _connectionString = string.Empty;
        }


        public DefaultDbContextFactory(
    IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(AppSettingsConstant.DefaultConnectionString);
        }



        /// <summary>
        /// Design Time DbContext (e.g Migrations)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ApplicationDbContext CreateContext()
        {
            var options = GetDbContextOptions(_connectionString!);
            return new ApplicationDbContext(options);
        }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile(AppSettingsConstant.AppSettingFileName, optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(AppSettingsConstant.AspNetCoreEnvironment)}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

            return new ApplicationDbContext(GetDbContextOptions(configuration.GetConnectionString(AppSettingsConstant.DefaultConnectionString)!));

        }





        /// <summary>
        /// Build DB context options
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private DbContextOptions<ApplicationDbContext> GetDbContextOptions(string connectionString)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseLazyLoadingProxies(false)
                .UseSqlServer(connectionString, providerOption =>
                {
                    providerOption.EnableRetryOnFailure(
                        maxRetryCount: 0,
                        maxRetryDelay: TimeSpan.FromSeconds(20),
                        errorNumbersToAdd: null);
                    providerOption.MigrationsHistoryTable(ApplicationDbContext.DefaultMigrationsHistoryTable,
                        ApplicationDbContext.DefaultSchema);
                    providerOption.CommandTimeout(150);
                }).UseLoggerFactory(new NLogLoggerFactory());


            return dbContextOptionsBuilder.Options;
        }
    }
}
