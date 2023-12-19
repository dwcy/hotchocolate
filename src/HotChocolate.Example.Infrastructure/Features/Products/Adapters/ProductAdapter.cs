using HotChocolate.Example.Domain.Entities;
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

	public async Task<ProductDto> GetProductById(int productId) => await _ProductRepository.GetProduct(productId);

	public IQueryable<ProductDto> GetProductsQueryable() => _ProductRepository.GetProductQueryable();

	public async Task<ProductResponse> CreateProduct(ProductCreateRequest productRequest)
	{
		var mappedToProduct = _mapper.Map<Product>(productRequest);

		var productDto = await _ProductRepository.CreateProduct(mappedToProduct);

		var productResponse = _mapper.Map<ProductResponse>(productDto);

		return productResponse;
	}

	public async Task<ProductResponse> UpdateProduct(ProductUpdateRequest productRequest)
	{
		var product = _mapper.Map<Product>(productRequest);

		var productDto = await _ProductRepository.UpdateProduct(product);

		var productResponse = _mapper.Map<ProductResponse>(productDto);

		return productResponse;
	}

	public async Task<bool> RemoveProduct(int productId)
	{
		var success = await _ProductRepository.RemoveProduct(productId);
		return success;
	}
}
