using HotChocolate.Example.Domain.Contracts;
using HotChocolate.Example.Domain.DTO;

namespace HotChocolate.Example.Domain.Interfaces.Products.Adapters;

public interface IProductAdapter
{
	Task<ProductDTO> GetProductById(int productId);
	IQueryable<ProductDTO> GetProductsQueryable();
	Task<ProductDTO> CreateProduct(CreateProductRequest product);
	Task<ProductDTO> UpdateProduct(UpdateProductRequest product);
}