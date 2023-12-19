using HotChocolate.Example.Domain.Products.Contracts;
using HotChocolate.Example.Domain.Products.Interfaces;
using HotChocolate.Example.Infrastructure.Configurations.GraphQL;

namespace HotChocolate.Example.Infrastructure.Features.Products.Graphs;

//[Authorize(Roles = new[] { "Create" })]
[ExtendObjectType(typeof(BaseMutation))]
public class ProductMutations
{
	private readonly IProductAdapter _productAdapter;

	public ProductMutations(IProductAdapter productAdapter)
	{
		_productAdapter = productAdapter;
	}

	[GraphQLDescription("Create a new product item")]
	public async Task<ProductResponse> CreateProduct(ProductCreateRequest product)
	{
		try
		{
			var result = await _productAdapter.CreateProduct(product);
			return result;
		}
		catch (Exception e)
		{
			throw new GraphQLException(new Error("Create Product Failed", e.Message));
		}
	}

	public async Task<ProductResponse> UpdateProduct(ProductUpdateRequest product)
	{
		try
		{
			var result = await _productAdapter.UpdateProduct(product);
			return result;
		}
		catch (Exception e)
		{
			throw new GraphQLException(new Error("update Product Failed", e.Message));
		}
	}

	public async Task<bool> RemoveProduct(int productId)
	{
		try
		{
			var result = await _productAdapter.RemoveProduct(productId);
			return result;
		}
		catch (Exception e)
		{
			throw new GraphQLException(new Error("update Product Failed", e.Message));
		}
	}
}
