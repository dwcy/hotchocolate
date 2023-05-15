using HotChocolate.Example.Infrastructure.Configurations.DependencyInjection;
using HotChocolate.Example.Infrastructure.Configurations.GraphQL;
using HotChocolate.Example.Infrastructure.Configurations.Mappers;
using HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;

namespace HotChocolate.Example.Tests.Base.Configurations;

public static class IntegrationTestsDependencies
{
	public static IServiceCollection RegisterIntegrationTestDependencies(this IServiceCollection services)
	{
		try
		{
			services
			.AddInfrastructureDependencies(null) //TODO handle null 
			.AddAllMappers()
			.AddPooledDbContextFactory<AdventureWorksContext>(SqlLiteInMemoryDatabase)
			.AddHotChocolateGraphQL()
			.AddAuthorization()
			.AddLogging()
			.AddScoped<IHttpContextAccessor, FakeContextAccessor>()
			.AddSingleton(sp => new RequestExecutorProxy(sp.GetRequiredService<IRequestExecutorResolver>(), Schema.DefaultName));
		}
		catch (Exception e)
		{
			throw new Exception(e.Message);
		}

		return services;
	}

	static readonly Action<DbContextOptionsBuilder> SqlLiteInMemoryDatabase = (options) =>
	{
		var connection = new SqliteConnection("DataSource=:memory:");
		connection.Open();
		options.UseSqlite(connection);
		options.EnableSensitiveDataLogging();
	};
}
