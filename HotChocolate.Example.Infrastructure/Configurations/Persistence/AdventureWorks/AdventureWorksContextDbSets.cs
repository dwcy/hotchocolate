namespace HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;

public partial class AdventureWorksContext : DbContext
{
	public virtual DbSet<Address> Addresses { get; set; }

	public virtual DbSet<BuildVersion> BuildVersions { get; set; }

	public virtual DbSet<Customer> Customers { get; set; }

	public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

	public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

	public virtual DbSet<Product> Products { get; set; }

	public virtual DbSet<ProductCategory> ProductCategories { get; set; }

	public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }

	public virtual DbSet<ProductModel> ProductModels { get; set; }

	public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }

	public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

	public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

	public virtual DbSet<VGetAllCategory> VGetAllCategories { get; set; }

	public virtual DbSet<VProductAndDescription> VProductAndDescriptions { get; set; }

	public virtual DbSet<VProductModelCatalogDescription> VProductModelCatalogDescriptions { get; set; }
}
