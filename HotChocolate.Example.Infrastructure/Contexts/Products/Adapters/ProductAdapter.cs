using HotChocolate.Example.Domain.Contracts;
using HotChocolate.Example.Domain.DTO;
using HotChocolate.Example.Domain.Interfaces.Products.Adapters;
using HotChocolate.Example.Domain.Interfaces.Products.Repositories;

namespace HotChocolate.Example.Infrastructure.Contexts.Products.Adapters;

/// <summary>
/// Product Adapter 
/// </summary>
public class ProductAdapter : IProductAdapter
{
	private readonly IMapper _mapper;
	private readonly IProductRepository _ProductRepository;

	public ProductAdapter(IProductRepository productRepository, IMapper mapper)
	{
		_ProductRepository = productRepository;
		_mapper = mapper;
	}

	public async Task<ProductDTO> GetProductById(int ProductId) => await _ProductRepository.GetProduct(ProductId);

	public IQueryable<ProductDTO> GetProductsQueryable() => _ProductRepository.GetProductQueryable();

	public async Task<ProductDTO> CreateProduct(CreateProductRequest Product)
	{
		var mappedToProduct = _mapper.Map<Product>(Product);

		return await _ProductRepository.CreateProduct(mappedToProduct);
	}

	public async Task<ProductDTO> UpdateProduct(ProductDTO Product)
	{
		var mappedToProduct = _mapper.Map<Product>(Product);

		return await _ProductRepository.UpdateProduct(mappedToProduct);
	}

	public Task<ProductDTO> UpdateProduct(UpdateProductRequest product)
	{
		throw new NotImplementedException();
	}
}
