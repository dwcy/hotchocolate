using HotChocolate.Example.Domain.Products.Contracts;
using HotChocolate.Example.Domain.Products.DTO;

namespace HotChocolate.Example.Domain.Products.Interfaces;

public interface IProductAdapter
{
	Task<ProductDto> GetProductById(int productId);
	IQueryable<ProductDto> GetProductsQueryable();
	Task<ProductDto> CreateProduct(ProductCreateRequest product);
	Task<ProductDto> UpdateProduct(ProductUpdateRequest product);
}