using HotChocolate.Example.Domain.DTO;
using HotChocolate.Example.Domain.Interfaces.Products.Adapters;

namespace HotChocolate.Example.Infrastructure.Configurations.GraphQL.Queries;

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
	public IQueryable<ProductDTO> GetProducts()
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
	public async Task<ProductDTO> GetProduct(int ProductId)
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
