using HotChocolate.Example.Domain.Contracts;
using HotChocolate.Example.Domain.Interfaces.Products.Adapters;
using HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;
using HotChocolate.Example.Tests.Base.Configurations;
using HotChocolate.Example.Tests.Base.Helpers;

namespace HotChocolate.Example.Tests.Base
{
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

			InitFreshDatabase();
			await SeedBaseData();
		}

		private void InitFreshDatabase()
		{
			var dbContext = Services.GetRequiredService<IDbContextFactory<AdventureWorksContext>>();
			using var context = dbContext.CreateDbContext();
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
		}

		private async Task SeedBaseData()
		{
			var productAdapter = Services.GetRequiredService<IProductAdapter>();

			var createProductRequest = new CreateProductRequest();

			createProductRequest.Name = "Test Product"; //TODO: Fill up with more data

			await productAdapter.CreateProduct(createProductRequest);
		}


	}
}