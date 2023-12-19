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

			var response = await GetProduct(context, product.ProductId);
			context.ChangeTracker.Clear();

			return response;
		}
		catch (Exception e) { throw new Exception($"Could not create product: Reason: {e.InnerException.Message}"); }
	}

	public async Task<bool> RemoveProduct(int productId)
	{
		try
		{
			await using var context = await _context.CreateDbContextAsync();
			var productToRemove = await context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
			if (productToRemove == null)
				throw new Exception("Product not found");

			context.Products.Remove(productToRemove);
			var rowsAffected = await context.SaveChangesAsync();

			return rowsAffected > 0;
		}
		catch (Exception e) { throw new Exception($"Could not remove product: Reason: {e.InnerException.Message ?? e.Message}"); }
	}

	public async Task<ProductDto> UpdateProduct(Product product)
	{
		if (product == null)
			throw new ArgumentNullException(nameof(Product));

		try
		{
			await using var context = await _context.CreateDbContextAsync();

			var entity = await context.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId);
			if (entity == null)
				throw new Exception("Product not found");

			entity.Name = product.Name;
			entity.ProductNumber = product.ProductNumber ?? entity.ProductNumber;
			context.Products.Update(entity);

			await context.SaveChangesAsync();

			context.ChangeTracker.Clear();

			return await GetProduct(context, product.ProductId);
		}
		catch (DbUpdateException e) { throw new Exception($"Could not update product: Reason: {e.InnerException.Message}"); }
	}

	private async Task<ProductDto> GetProduct(AdventureWorksContext context, int productId)
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
