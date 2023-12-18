using HotChocolate.Example.Application;
using HotChocolate.Example.Domain.Products.Interfaces;
using HotChocolate.Example.Infrastructure.Configurations.Cors;
using HotChocolate.Example.Infrastructure.Configurations.GraphQL;
using HotChocolate.Example.Infrastructure.Configurations.Mappers;
using HotChocolate.Example.Infrastructure.Configurations.Persistence;
using HotChocolate.Example.Infrastructure.Features.Products.Adapters;
using HotChocolate.Example.Infrastructure.Features.Products.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Example.Infrastructure.Configurations.DependencyInjection;

public static class RegisterInfrastructureDependencies
{
	public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration config)
	{
		return services
			.RegisterInfrastructure(config)
			.RegisterProductsContext();
	}

	/// <summary>
	/// Base infrastructure configurations
	/// </summary>
	/// <param name="services"></param>
	/// <param name="config"></param>
	/// <returns></returns>
	internal static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration config)
	{
		return services
		.AddHttpContextAccessor()
		.AddCorsConfigurations(config)
		.AddAutoMapperConfigurations()
		.AddDatabaseContexts(config)
		.AddHotChocolateGraphQL();
	}

	/// <summary>
	/// Register Product related Services, Adapters and Repositories
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	internal static IServiceCollection RegisterProductsContext(this IServiceCollection services)
	{
		return services
			.AddScoped<IProductRepository, ProductRepository>()
			.AddScoped<IProductAdapter, ProductAdapter>()
			.AddScoped<IProductService, ProductService>();
	}
}
