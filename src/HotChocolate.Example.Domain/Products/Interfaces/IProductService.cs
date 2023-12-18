using HotChocolate.Example.Domain.Products.Contracts;

namespace HotChocolate.Example.Domain.Products.Interfaces;
public interface IProductService
{
	Task<ProductResponse> GetProducts(int productId);
}
