using HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HotChocolate.Example.Infrastructure.Configurations.Persistence;

public static class EntityFrameworkDbContextConfigurations
{
	static readonly ILoggerFactory LogFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

	public static IServiceCollection AddDatabaseContexts(this IServiceCollection services, IConfiguration config)
	{
		if (config == null)
			return services;

		var connectionString = config.GetConnectionString(nameof(AdventureWorksContext));

		if (string.IsNullOrEmpty(connectionString))
			throw new Exception("Connection string missing");

		services.AddPooledDbContextFactory<AdventureWorksContext>(options =>
		{
			options.UseSqlServer(connectionString, x => x.UseNetTopologySuite());
			options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
#if DEBUG
			options.UseLoggerFactory(LogFactory).EnableSensitiveDataLogging();
#endif
		});

		return services;
	}
}
