namespace HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;

public partial class AdventureWorksContext : DbContext
{
	public virtual DbSet<Product> Products { get; set; }

	public virtual DbSet<ProductCategory> ProductCategories { get; set; }

	public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }

	public virtual DbSet<ProductModel> ProductModels { get; set; }

	public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }
}
