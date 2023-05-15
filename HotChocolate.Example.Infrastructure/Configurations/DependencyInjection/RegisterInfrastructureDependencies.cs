using HotChocolate.Example.Domain.Interfaces.Products.Adapters;
using HotChocolate.Example.Domain.Interfaces.Products.Repositories;
using HotChocolate.Example.Infrastructure.Configurations.Cors;
using HotChocolate.Example.Infrastructure.Configurations.GraphQL;
using HotChocolate.Example.Infrastructure.Configurations.Mappers;
using HotChocolate.Example.Infrastructure.Configurations.Persistence;
using HotChocolate.Example.Infrastructure.Contexts.Products.Adapters;
using HotChocolate.Example.Infrastructure.Contexts.Products.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Example.Infrastructure.Configurations.DependencyInjection;
public static class RegisterInfrastructureDependencies
{
	public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration config)
	{
		return services
			.RegisterInfrastructure(config)
			.RegisterAdaptersAndRepositories()
			.RegisterDomainServices()
			;
	}

	/// <summary>
	/// Base infrastructure configurations
	/// </summary>
	/// <param name="services"></param>
	/// <param name="config"></param>
	/// <returns></returns>
	public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration config)
	{
		services
		.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
		.AddHttpContextAccessor()
		.ConfigureCors(config)
		.AddAllMappers()
		.AddDatabaseContexts(config);
		services.AddHotChocolateGraphQL();

		return services;
	}

	/// <summary>
	/// Register Infrastructure Adapters and Repositories
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	public static IServiceCollection RegisterAdaptersAndRepositories(this IServiceCollection services)
	{
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IProductAdapter, ProductAdapter>();

		return services;
	}

	/// <summary>
	/// Register Domain Services
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
	{
		//Add when exists

		return services;
	}
}
