using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Example.Infrastructure.Configurations.Mappers;

public static class AutomapperConfigurationsExtension
{
	public static IServiceCollection AddAutoMapperConfigurations(this IServiceCollection services)
	{
		services
		.AddSingleton(provider =>
			new MapperConfiguration(cfg =>
			{
				cfg.ConstructServicesUsing(t => ActivatorUtilities.CreateInstance(provider, t));
				cfg.AddProfile(new ProductMappings());
			})
		.CreateMapper());

		return services;
	}
}
