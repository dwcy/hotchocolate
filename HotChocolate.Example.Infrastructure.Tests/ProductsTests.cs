using HotChocolate.Example.Tests.Base;
using HotChocolate.Example.Tests.Base.Helpers;
using HotChocolate.Example.Tests.Base.Models;

namespace HotChocolate.Example.Tests;

[Parallelizable]
public class ProductsTests : TestBase
{
	[Test]
	public async Task GivenProductQuery_WhenFiltering_ThenNoErrors()
	{
		//Arrange
		var query = await _fileFetcher.GetGraphQuery(TestGraphQuery.ProductQuery);

		//Act
		var result = await ExecuteGraphQLQuery<Products>(x => x.SetQuery(query));

		//Assert
		result.Errors.Should().BeNullOrEmpty();
	}

	[Test]
	public async Task GivenProductQuery_WhenFiltering_ThenOneResultFromDatabase()
	{
		//Arrange
		var query = await _fileFetcher.GetGraphQuery(TestGraphQuery.ProductQuery);

		//Act
		var result = await ExecuteGraphQLQuery<Products>(x => x.SetQuery(query));

		//Assert
		var product = result.Data!.Result!.Nodes?.FirstOrDefault();

		result.Data.Result.Nodes.Should().HaveCount(1);
		product?.Name.Should().NotBeNullOrEmpty();
	}
}
