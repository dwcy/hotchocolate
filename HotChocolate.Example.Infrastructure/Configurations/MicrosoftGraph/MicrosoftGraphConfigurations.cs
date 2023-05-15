using Azure.Identity;
using HotChocolate.Example.Infrastructure.Configurations.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;

namespace HotChocolate.Example.Infrastructure.Configurations.MicrosoftGraph;
public static class MicrosoftGraphConfigurations
{
	public static IServiceCollection AddMicrosoftGraph(this IServiceCollection services, IConfiguration configuration)
	{
		var graphClient = CreateGraphClient(configuration);
		services.AddSingleton(graphClient);

		return services;
	}

	public static GraphServiceClient CreateGraphClient(IConfiguration configuration)
	{
		if (configuration is null)
			throw new Exception("Config is empty");

		var azureConfig = configuration.GetSection("AzureAdGraph").Get<AzureAdConfigurationSection>();

		var authProvider = new AuthorizationCodeCredential(azureConfig.TenantId, azureConfig.ClientId, azureConfig.ClientSecret, azureConfig.SignedOutCallbackPath);
		var graphClient = new GraphServiceClient(authProvider);

		return graphClient;
	}
}
