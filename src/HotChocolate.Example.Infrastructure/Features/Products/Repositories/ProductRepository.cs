using AutoMapper.QueryableExtensions;
using HotChocolate.Example.Domain.Products.DTO;
using HotChocolate.Example.Domain.Products.Interfaces;
using HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;

namespace HotChocolate.Example.Infrastructure.Features.Products.Repositories;

internal sealed class ProductRepository : IProductRepository
{
	private readonly IDbContextFactory<AdventureWorksContext> _context;
	private readonly IMapper _mapper;

	public ProductRepository(IDbContextFactory<AdventureWorksContext> context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<ProductDto> GetProduct(int productId)
	{
		await using var context = await _context.CreateDbContextAsync();

		var result = await ProductIncludingAllAsQuerable(context)
			.ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
			.FirstOrDefaultAsync(x => x.ProductId == productId);

		return result;
	}

	public IQueryable<ProductDto> GetProductQueryable()
	{
		return _context
				.CreateDbContext()
				.Products
				.AsNoTracking()
				.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
	}

	public async Task<ProductDto> CreateProduct(Product product)
	{
		if (product == null)
			throw new ArgumentNullException(nameof(Product));

		try
		{
			await using var context = await _context.CreateDbContextAsync();
			await context.Products.AddAsync(product);
			await context.SaveChangesAsync();

			var response = await GetProducts(context, product.ProductId);
			context.ChangeTracker.Clear();

			return response;
		}
		catch (Exception e) { throw new Exception($"Could not create product: Reason: {e.InnerException.Message}"); }
	}

	public async Task<ProductDto> UpdateProduct(Product product)
	{
		if (product == null)
			throw new ArgumentNullException(nameof(Product));

		try
		{
			await using var context = await _context.CreateDbContextAsync();

			context.Products.Update(product);

			await context.SaveChangesAsync();

			var result = await GetProducts(context, product.ProductId);
			context.ChangeTracker.Clear();

			return result;
		}
		catch (DbUpdateException) { throw new Exception($"Cannot update product with id: {product.ProductId}, reason: product does not exist in the database"); }
		catch (Exception e) { throw new Exception($"Could not update product: Reason: {e.InnerException.Message}"); }
	}

	private async Task<ProductDto> GetProducts(AdventureWorksContext context, int productId)
		=> await _mapper
		.ProjectTo<ProductDto>(
			ProductIncludingAllAsQuerable(context)
			.Where(t => t.ProductId == productId)
		).FirstOrDefaultAsync();

	private IQueryable<Product> ProductIncludingAllAsQuerable(AdventureWorksContext context)
		 => context
		.Products
		.AsNoTracking()
		.Include(x => x.ProductCategory)
		.Include(x => x.ProductModel);
}
