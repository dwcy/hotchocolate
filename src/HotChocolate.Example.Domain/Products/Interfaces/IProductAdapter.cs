using HotChocolate.Example.Domain.Products.Contracts;
using HotChocolate.Example.Domain.Products.DTO;

namespace HotChocolate.Example.Domain.Products.Interfaces;

public interface IProductAdapter
{
	/// <summary>
	/// Get full product information by Id
	/// </summary>
	/// <param name="productId"></param>
	/// <returns></returns>
	Task<ProductDto> GetProductById(int productId);

	/// <summary>
	/// Use for advanced search query and filters on product database
	/// </summary>
	/// <returns></returns>
	IQueryable<ProductDto> GetProductsQueryable();

	/// <summary>
	/// Create a new product in the database
	/// </summary>
	/// <param name="productCreateRequest"></param>
	/// <returns></returns>
	Task<ProductResponse> CreateProduct(ProductCreateRequest productCreateRequest);

	/// <summary>
	/// Update product information by productId
	/// </summary>
	/// <param name="productUpdateRequest"></param>
	/// <returns></returns>
	Task<ProductResponse> UpdateProduct(ProductUpdateRequest productUpdateRequest);

	/// <summary>
	/// Removes the product from the database
	/// </summary>
	/// <param name="productId"></param>
	/// <returns></returns>
	Task<bool> RemoveProduct(int productId);
}