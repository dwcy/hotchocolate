using DataAnnotatedModelValidations;
using HotChocolate.Example.Infrastructure.Configurations.GraphQL.Mutations;
using HotChocolate.Example.Infrastructure.Configurations.GraphQL.Queries;
using HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;
using HotChocolate.Types.Pagination;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Example.Infrastructure.Configurations.GraphQL;

public static class GraphqlConfigurationsExtensions
{
	public static IServiceCollection AddHotChocolateGraphQL(this IServiceCollection services)
	{
		services.AddGraphQLServer() //creating hotchocolate graph server
			.AddDataAnnotationsValidator() //auto validates models in middleware (no need to use modelstate is valid)
			.RegisterDbContext<AdventureWorksContext>() //Hot chocolate can query the database context
														//.AddAuthorization() //auth middleware for graphql
			.AddQueryType<BaseQuery>() //Base querty will create a path in the graph ui /query/... where all mutations exists
			.AddMutationType<BaseMutation>() //Base mutations will create a path in the graph ui /mutations/... where all mutations exists
			.AddTypeExtension<ProductQueries>() //your graph controller containing Query endpoints
			.AddTypeExtension<ProductMutations>() //your graph controller containing Mutations endpoints
			.AddProjections() //using projects via automapper for perfomant queries
			.SetPagingOptions(new PagingOptions
			{
				DefaultPageSize = 10,
				IncludeTotalCount = true,
				MaxPageSize = 100
			}) //setup default pagination options, use in your graph controller with the paging attribute
			.AddFiltering() //Use of Filter attribute in your controller
			.AddSorting(); //Use Sorting Attribute in your controller

		return services;
	}
}