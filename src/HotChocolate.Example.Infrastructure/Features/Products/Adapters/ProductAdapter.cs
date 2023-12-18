using HotChocolate.Example.Domain.Products.Contracts;
using HotChocolate.Example.Domain.Products.DTO;
using HotChocolate.Example.Domain.Products.Interfaces;

namespace HotChocolate.Example.Infrastructure.Features.Products.Adapters;

public class ProductAdapter : IProductAdapter
{
	private readonly IMapper _mapper;
	private readonly IProductRepository _ProductRepository;

	public ProductAdapter(IProductRepository productRepository, IMapper mapper)
	{
		_ProductRepository = productRepository;
		_mapper = mapper;
	}

	public async Task<ProductDto> GetProductById(int ProductId) => await _ProductRepository.GetProduct(ProductId);

	public IQueryable<ProductDto> GetProductsQueryable() => _ProductRepository.GetProductQueryable();

	public async Task<ProductDto> CreateProduct(ProductCreateRequest Product)
	{
		var mappedToProduct = _mapper.Map<Product>(Product);

		return await _ProductRepository.CreateProduct(mappedToProduct);
	}

	public async Task<ProductDto> UpdateProduct(ProductDto Product)
	{
		var mappedToProduct = _mapper.Map<Product>(Product);

		return await _ProductRepository.UpdateProduct(mappedToProduct);
	}

	public Task<ProductDto> UpdateProduct(ProductUpdateRequest product)
	{
		throw new NotImplementedException();
	}
}
