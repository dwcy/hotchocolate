using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Example.Infrastructure.Configurations.Cors;

public static class CorsConfigurationExtensions
{
	public static IServiceCollection AddCorsConfigurations(this IServiceCollection services, IConfiguration configuration)
	{
		if (configuration == null)
			return services;

		var allowedOrigin = GetCorsFromConfig(configuration);

		if (allowedOrigin.Any())
			services.AddCors(allowedOrigin!);

		return services;
	}

	static string?[] GetCorsFromConfig(IConfiguration configuration) =>
		configuration.GetSection("AllowedOrigins")
		.AsEnumerable()
		.Select(x => x.Value)
		.Where(x => !string.IsNullOrEmpty(x))
		.ToArray();

	static IServiceCollection AddCors(this IServiceCollection services, string[] allowedOrigins) =>
		services.AddCors(options =>
		{
			options.AddDefaultPolicy(builder =>
			{
				builder.WithOrigins(allowedOrigins)
					.AllowAnyMethod()
					.AllowAnyHeader();
			});
		});
}
