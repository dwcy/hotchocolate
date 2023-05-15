using HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Example.Infrastructure.Configurations.Persistence;

/// <summary>
/// Use this extension method to run EF migrations on startup
/// </summary>
public static class RunEFMigrationExtension
{
	public static WebApplication MigrateDatabaseChanges(this WebApplication app)
	{
		if (app == null)
			throw new ArgumentNullException("WebApplication is null");

		var dbContext = app.Services.GetRequiredService<IDbContextFactory<AdventureWorksContext>>();

		dbContext.CreateDbContext().Database.Migrate();

		return app;
	}
}
