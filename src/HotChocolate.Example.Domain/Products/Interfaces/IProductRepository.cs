using HotChocolate.Example.Domain.Entities;
using HotChocolate.Example.Domain.Products.DTO;

namespace HotChocolate.Example.Domain.Products.Interfaces;

public interface IProductRepository
{
	Task<ProductDto> GetProduct(int productId);
	IQueryable<ProductDto> GetProductQueryable();
	Task<ProductDto> CreateProduct(Product product);
	Task<ProductDto> UpdateProduct(Product product);
	Task<bool> RemoveProduct(int productId);
}