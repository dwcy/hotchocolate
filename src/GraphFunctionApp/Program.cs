using HotChocolate.AspNetCore;
using HotChocolate.Example.Infrastructure.Configurations.DependencyInjection;
using HotChocolate.Example.Infrastructure.Configurations.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting;

namespace GraphFunctionApp;

public class Program
{
	public static IConfiguration Configuration;

	public static void Main(string[] args)
	{
		var host = new HostBuilder()
		.ConfigureAppConfiguration(builder =>
		{
			builder.AddJsonFile("settings.json", optional: true, reloadOnChange: true);
			builder.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
			builder.AddEnvironmentVariables();
		})
		 .ConfigureServices((context, services) =>
		 {
			 services.AddInfrastructureDependencies(Configuration);
		 })
		 .Build();

		host.Run();
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
		.UseRouting()
		.UseHttpsRedirection()
		.UseGraphiQL()
		.UseEndpoints(endpoints =>
		{
			var graphEndPointConfig = endpoints.MapGraphQL().WithOptions(new() { EnableSchemaRequests = true });

			if (!isDevelopment)
				graphEndPointConfig.RequireAuthorization();
		});


		app.MigrateDatabaseChanges();

		app.Run();
	}
}