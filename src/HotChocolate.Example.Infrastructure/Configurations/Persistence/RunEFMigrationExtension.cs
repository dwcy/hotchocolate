using HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Example.Infrastructure.Configurations.Persistence;

/// <summary>
/// Use this extension method to run EF migrations on startup
/// </summary>
public static class RunEFMigrationExtension
{
	public static WebApplication MigrateDatabaseChanges(this WebApplication app)
	{
		try
		{
			var dbContext = app.Services.GetRequiredService<IDbContextFactory<AdventureWorksContext>>();

			using var context = dbContext.CreateDbContext();
			//context.Database.EnsureCreated();
			context.Database.Migrate();
		}
		catch (SqlException error)  { throw error; /* handle error */}
		catch (Exception error) { throw error; /* handle error */}

		return app;
	}
}
