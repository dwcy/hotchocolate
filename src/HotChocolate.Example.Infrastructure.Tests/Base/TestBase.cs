using Bogus;
using Bogus.DataSets;
using HotChocolate.Example.Domain.Products.Contracts;
using HotChocolate.Example.Domain.Products.Interfaces;
using HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;
using HotChocolate.Example.Tests.Base.Configurations;
using HotChocolate.Example.Tests.Base.Helpers;

namespace HotChocolate.Example.Tests.Base;

/// <summary>
/// Type: Base test class
/// Containing: setup for hot chocolate integration tests
/// Usage: graphql integration/unit tests
/// </summary>
public abstract class TestBase : GraphQLQueryExecutor
{
	public IServiceProvider Services;

	[SetUp]
	public async Task SetupAsync()
	{
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

		Services = new ServiceCollection()
			.RegisterIntegrationTestDependencies()
			.BuildServiceProvider();

		RunSetupGraphQLQueryExecutor(Services);

		await InitFreshDatabase();
		await SeedBaseData();
	}

	private async Task InitFreshDatabase()
	{
		try
		{
			var dbContext = Services.GetRequiredService<IDbContextFactory<AdventureWorksContext>>();
			var context = await dbContext.CreateDbContextAsync();
			await context.Database.EnsureDeletedAsync();
			await context.Database.EnsureCreatedAsync();
			context.Dispose();
		}
		catch (Exception ex)
		{

			throw ex;
		}

	}

	private async Task SeedBaseData()
	{
		var productAdapter = Services.GetRequiredService<IProductAdapter>();


		var createProductRequest = CreateProductRequestUsingBogus(16).GetEnumerator();
		while(createProductRequest.MoveNext())
		{
			await productAdapter.CreateProduct(createProductRequest.Current);
		}
	}

	IEnumerable<ProductCreateRequest> CreateProductRequestUsingBogus(int totalToGenerate = 100)
	{
		var bogusProducts = new List<ProductCreateRequest>();

		Randomizer.Seed = new Random(8675309);
		var commerce = new Commerce();

		for (int ProductIds = 0; ProductIds < totalToGenerate; ProductIds++)
		{
			var bogusProduct = new Faker<ProductCreateRequest>()
			.StrictMode(true)
			.RuleFor(p => p.ProductNumber, f => ProductIds++)
			.RuleFor(p => p.Name, f => f.PickRandom(commerce.ProductName()));

			yield return bogusProduct;
		}
	}
}