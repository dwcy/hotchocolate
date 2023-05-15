using HotChocolate.Example.Domain.DTO;
using HotChocolate.Example.Domain.Entities;

namespace HotChocolate.Example.Domain.Interfaces.Products.Repositories;

public interface IProductRepository
{
	Task<ProductDTO> GetProduct(int productId);
	IQueryable<ProductDTO> GetProductQueryable();
	Task<ProductDTO> CreateProduct(Product product);
	Task<ProductDTO> UpdateProduct(Product product);
}