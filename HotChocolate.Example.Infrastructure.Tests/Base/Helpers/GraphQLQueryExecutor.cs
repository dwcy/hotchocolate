using HotChocolate.Example.Tests.Base.Models;
using HotChocolate.Example.Tests.Base.TestAttributes;

namespace HotChocolate.Example.Tests.Base.Helpers;

public abstract class GraphQLQueryExecutor
{
	public GraphQueryFileFetcher _fileFetcher { get; set; } = new GraphQueryFileFetcher();
	IServiceProvider? _services;

	RequestExecutorProxy? _executor { get; set; }

	/// <summary>
	/// Call this from nunit setup
	/// </summary>
	/// <param name="services"></param>
	public void RunSetupGraphQLQueryExecutor(IServiceProvider services)
	{
		_services = services;
		_executor = _services.GetRequiredService<RequestExecutorProxy>();
	}

	/// <summary>
	/// Executing graphql queries and returning a deserialized object of T
	/// </summary>
	/// <param name="configureRequest"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public async Task<GraphqlResponse<T>> ExecuteGraphQLQuery<T>(Action<IQueryRequestBuilder> configureRequest, CancellationToken cancellationToken = default)
	{
		if (configureRequest == null)
			throw new ArgumentNullException(nameof(configureRequest));

		try
		{
			await using var scope = _services!.CreateAsyncScope();

			var requestBuilder = new QueryRequestBuilder();
			requestBuilder.SetServices(scope.ServiceProvider);
			configureRequest(requestBuilder);

			var request = requestBuilder
				//	.AddGlobalState(nameof(ClaimsPrincipal), FakeContextAccessor.CreateFakePrincipal()) //TODO Is this needed after implementing FakeHttpContextAccessor.cs ?
				.Create();

			var response = await _executor.ExecuteAsync(request, cancellationToken);

			var queryResult = response.ExpectQueryResult();
			var jsonResult = queryResult.ToJson();

			var result = JsonConvert
				.DeserializeObject<GraphqlResponse<T>>(
					jsonResult,
					new JsonSerializerSettings
					{
						ContractResolver = new GenericJsonPropertyResolver(),
						TypeNameHandling = TypeNameHandling.Objects,
						Formatting = Formatting.Indented,
					}
				);

			return result!;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}
