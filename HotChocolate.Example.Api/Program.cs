using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using HotChocolate.Example.Infrastructure.Configurations.DependencyInjection;
using HotChocolate.Example.Infrastructure.Configurations.Logging;
using HotChocolate.Example.Infrastructure.Configurations.Persistence;
using HotChocolate.Example.Infrastructure.Configurations.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace HotChocolate.Example.Api;

public class Program
{
	public static IConfiguration Configuration;

	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.AddAppSettings(out Configuration);
		builder.AddLogging();

		ConfigureServices(builder.Services);

		RunWebApplication(builder);
	}

	public static void ConfigureServices(IServiceCollection services)
	{

		//services.AddMicrosoftIdentityWebApiAuthentication(Configuration);
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		});

		services.AddAuthorization();
		services.AddInfrastructureDependencies(Configuration);
	}

	/// <summary>
	/// Build and start the web application
	/// </summary>
	/// <param name="builder"></param>
	static void RunWebApplication(WebApplicationBuilder builder)
	{
		var app = builder.Build();

		var isDevelopment = app.Environment.IsDevelopment();

		app
		.UseHttpsRedirection()
		.UseVoyager(new VoyagerOptions { Path = "/voyager", QueryPath = "/graphql" })
		.UseRouting()
		.UseHttpsRedirection()
		.UseAuthentication()
		.UseAuthorization()
		.UseGraphiQL()
		.UseEndpoints(endpoints =>
		{
			var graphEndPoiuntConfig = endpoints.MapGraphQL().WithOptions(new() { EnableSchemaRequests = true });

			if (!isDevelopment)
				graphEndPoiuntConfig.RequireAuthorization();
		});

		if (isDevelopment)
			app
			.UseDeveloperExceptionPage()
			.UseVoyager(new VoyagerOptions { Path = "/voyager", QueryPath = "/graphql" });

		app.MigrateDatabaseChanges();

		app.Run();
	}
}