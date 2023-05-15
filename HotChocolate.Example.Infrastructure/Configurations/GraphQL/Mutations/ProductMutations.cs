
using HotChocolate.Example.Domain.Contracts;
using HotChocolate.Example.Domain.DTO;
using HotChocolate.Example.Domain.Interfaces.Products.Adapters;

namespace HotChocolate.Example.Infrastructure.Configurations.GraphQL.Mutations;

//[Authorize(Roles = new[] { "Create" })]
[ExtendObjectType(typeof(BaseMutation))]
public class ProductMutations
{
	private readonly IProductAdapter _productAdapter;

	public ProductMutations(IProductAdapter productAdapter)
	{
		_productAdapter = productAdapter;
	}

	public async Task<ProductDTO> CreateProduct(CreateProductRequest createProduct)
	{
		try
		{
			var result = await _productAdapter.CreateProduct(createProduct);
			return result;
		}
		catch (Exception e)
		{
			throw new GraphQLException(new Error("Create Product Failed", e.Message));
		}
	}

	public async Task<ProductDTO> UpdateProduct(UpdateProductRequest updateProduct)
	{
		try
		{
			var result = await _productAdapter.UpdateProduct(updateProduct);

			return result;
		}
		catch (Exception e)
		{
			throw new GraphQLException(new Error("update Product Failed", e.Message));
		}
	}
}
