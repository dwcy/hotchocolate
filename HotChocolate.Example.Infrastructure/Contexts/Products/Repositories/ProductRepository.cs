using AutoMapper.QueryableExtensions;
using HotChocolate.Example.Domain.DTO;
using HotChocolate.Example.Domain.Interfaces.Products.Repositories;
using HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;

namespace HotChocolate.Example.Infrastructure.Contexts.Products.Repositories;

/// <summary>
/// Product Repository
/// </summary>
public sealed class ProductRepository : IProductRepository
{
	private readonly IDbContextFactory<AdventureWorksContext> _context;
	private readonly IMapper _mapper;

	public ProductRepository(IDbContextFactory<AdventureWorksContext> context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<ProductDTO> GetProduct(int productId)
	{
		await using var context = await _context.CreateDbContextAsync();

		var result = await ProductIncludingAllAsQuerable(context)
			.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
			.FirstOrDefaultAsync(x => x.ProductId == productId);

		return result;
	}

	public IQueryable<ProductDTO> GetProductQueryable()
	{
		return _context
				.CreateDbContext()
				.Products
				.AsNoTracking()
				.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider);
	}

	public async Task<ProductDTO> CreateProduct(Product product)
	{
		if (product == null)
			throw new ArgumentNullException(nameof(Product));

		try
		{
			await using var context = await _context.CreateDbContextAsync();
			await context.Products.AddAsync(product);
			await context.SaveChangesAsync();

			var response = await GetProductAsDTO(context, product.ProductId);
			context.ChangeTracker.Clear();

			return response;
		}
		catch (Exception e) { throw new Exception($"Could not create product: Reason: {e.InnerException.Message}"); }
	}

	public async Task<ProductDTO> UpdateProduct(Product product)
	{
		if (product == null)
			throw new ArgumentNullException(nameof(Product));

		try
		{
			await using var context = await _context.CreateDbContextAsync();

			context.Products.Update(product);

			await context.SaveChangesAsync();

			var result = await GetProductAsDTO(context, product.ProductId);
			context.ChangeTracker.Clear();

			return result;
		}
		catch (DbUpdateException) { throw new Exception($"Cannot update product with id: {product.ProductId}, reason: product does not exist in the database"); }
		catch (Exception e) { throw new Exception($"Could not update product: Reason: {e.InnerException.Message}"); }
	}



	private async Task<ProductDTO> GetProductAsDTO(AdventureWorksContext context, int productId) =>
		await _mapper
			.ProjectTo<ProductDTO>(ProductIncludingAllAsQuerable(context).Where(t => t.ProductId == productId))
			.FirstOrDefaultAsync();

	private IQueryable<Product> ProductIncludingAllAsQuerable(AdventureWorksContext context) => context
		.Products
		.AsNoTracking()
		.Include(x => x.ProductCategory)
		.Include(x => x.SalesOrderDetails)
		.Include(x => x.ProductModel);

}
