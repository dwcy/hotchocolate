using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace HotChocolate.Example.Infrastructure.Configurations.Settings;

public static class ConfigSettingsExtensions
{
	public static WebApplicationBuilder AddAppSettings(this WebApplicationBuilder builder, out IConfiguration configuration)
	{
		builder?
			.Configuration
			.AddJsonFile("appsettings.json", true)
			.AddJsonFile(System.IO.Path.Combine("AppSettings", "appsettings.json"), optional: true, reloadOnChange: true)
			.AddJsonFile(System.IO.Path.Combine("AppSettings", "appsettings.development.json"), optional: true, reloadOnChange: true)

			.AddEnvironmentVariables();

		configuration = builder.Configuration;

		return builder;
	}

}
