using HotChocolate.Example.Domain.Products.DTO;
using HotChocolate.Example.Domain.Products.Interfaces;
using HotChocolate.Example.Infrastructure.Configurations.GraphQL;

namespace HotChocolate.Example.Infrastructure.Features.Products.Graphs;

//[Authorize(Roles = new[] { "Read" })]
[ExtendObjectType(typeof(BaseQuery))]
public class ProductQueries
{
	private readonly IProductAdapter _productAdapter;

	public ProductQueries(IProductAdapter productAdapter)
	{
		_productAdapter = productAdapter;
	}

	[GraphQLDescription("Products Paging and filtering"), UsePaging, UseProjection, UseFiltering, UseSorting] //The order matters => UsePaging > UseProjection > UseFiltering > UseSorting
	public IQueryable<ProductDto> GetProducts()
	{
		try
		{
			return _productAdapter.GetProductsQueryable();
		}
		catch (Exception e)
		{
			throw new GraphQLException(e.Message);
		}
	}

	[GraphQLDescription("Get single Product will all data"), UseProjection]
	public async Task<ProductDto> GetProduct(int ProductId)
	{
		try
		{
			return await _productAdapter.GetProductById(ProductId);
		}
		catch (Exception e)
		{
			throw new GraphQLException(e.Message);
		}
	}
}
