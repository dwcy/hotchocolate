namespace HotChocolate.Example.Infrastructure.Configurations.Persistence.AdventureWorks;

public partial class AdventureWorksContext : DbContext
{
	public AdventureWorksContext()
	{
	}

	public AdventureWorksContext(DbContextOptions<AdventureWorksContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		if (modelBuilder == null)
			return;

		modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

		modelBuilder.Entity<Product>(entity =>
		{
			entity.HasKey(e => e.ProductId).HasName("PK_Product_ProductID");

			entity.ToTable("Product", "SalesLT", tb => tb.HasComment("Products sold or used in the manfacturing of sold products."));

			entity.HasIndex(e => e.Name, "AK_Product_Name").IsUnique();

			entity.HasIndex(e => e.ProductNumber, "AK_Product_ProductNumber").IsUnique();

			entity.Property(e => e.ProductId)
				.HasComment("Primary key for Product records.")
				.HasColumnName("ProductID");
			entity.Property(e => e.Color)
				.HasMaxLength(15)
				.HasComment("Product color.");
			entity.Property(e => e.DiscontinuedDate)
				.HasComment("Date the product was discontinued.")
				.HasColumnType("datetime");
			entity.Property(e => e.ListPrice)
				.HasComment("Selling price.")
				.HasColumnType("money");
			entity.Property(e => e.ModifiedDate)
				.HasComment("Date and time the record was last updated.")
				.HasColumnType("datetime");
			entity.Property(e => e.Name)
				.HasMaxLength(50)
				.HasComment("Name of the product.");
			entity.Property(e => e.ProductCategoryId)
				.HasComment("Product is a member of this product category. Foreign key to ProductCategory.ProductCategoryID. ")
				.HasColumnName("ProductCategoryID");
			entity.Property(e => e.ProductModelId)
				.HasComment("Product is a member of this product model. Foreign key to ProductModel.ProductModelID.")
				.HasColumnName("ProductModelID");
			entity.Property(e => e.ProductNumber)
				.HasMaxLength(25)
				.HasComment("Unique product identification number.");
			entity.Property(e => e.SellEndDate)
				.HasComment("Date the product was no longer available for sale.")
				.HasColumnType("datetime");
			entity.Property(e => e.SellStartDate)
				.HasComment("Date the product was available for sale.")
				.HasColumnType("datetime");
			entity.Property(e => e.Size)
				.HasMaxLength(5)
				.HasComment("Product size.");
			entity.Property(e => e.StandardCost)
				.HasComment("Standard cost of the product.")
				.HasColumnType("money");
			entity.Property(e => e.ThumbNailPhoto).HasComment("Small image of the product.");
			entity.Property(e => e.ThumbnailPhotoFileName)
				.HasMaxLength(50)
				.HasComment("Small image file name.");
			entity.Property(e => e.Weight)
				.HasComment("Product weight.")
				.HasColumnType("decimal(8, 2)");

			entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products).HasForeignKey(d => d.ProductCategoryId);

			entity.HasOne(d => d.ProductModel).WithMany(p => p.Products).HasForeignKey(d => d.ProductModelId);
		});

		modelBuilder.Entity<ProductCategory>(entity =>
		{
			entity.HasKey(e => e.ProductCategoryId).HasName("PK_ProductCategory_ProductCategoryID");

			entity.ToTable("ProductCategory", "SalesLT", tb => tb.HasComment("High-level product categorization."));

			entity.HasIndex(e => e.Name, "AK_ProductCategory_Name").IsUnique();

			entity.HasIndex(e => e.Rowguid, "AK_ProductCategory_rowguid").IsUnique();

			entity.Property(e => e.ProductCategoryId)
				.HasComment("Primary key for ProductCategory records.")
				.HasColumnName("ProductCategoryID");
			entity.Property(e => e.ModifiedDate)
				.HasComment("Date and time the record was last updated.")
				.HasColumnType("datetime");
			entity.Property(e => e.Name)
				.HasMaxLength(50)
				.HasComment("Category description.");
			entity.Property(e => e.ParentProductCategoryId)
				.HasComment("Product category identification number of immediate ancestor category. Foreign key to ProductCategory.ProductCategoryID.")
				.HasColumnName("ParentProductCategoryID");
			entity.Property(e => e.Rowguid)
				.HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
				.HasColumnName("rowguid");

			entity.HasOne(d => d.ParentProductCategory).WithMany(p => p.InverseParentProductCategory)
				.HasForeignKey(d => d.ParentProductCategoryId)
				.HasConstraintName("FK_ProductCategory_ProductCategory_ParentProductCategoryID_ProductCategoryID");
		});

		modelBuilder.Entity<ProductDescription>(entity =>
		{
			entity.HasKey(e => e.ProductDescriptionId).HasName("PK_ProductDescription_ProductDescriptionID");

			entity.ToTable("ProductDescription", "SalesLT", tb => tb.HasComment("Product descriptions in several languages."));

			entity.HasIndex(e => e.Rowguid, "AK_ProductDescription_rowguid").IsUnique();

			entity.Property(e => e.ProductDescriptionId)
				.HasComment("Primary key for ProductDescription records.")
				.HasColumnName("ProductDescriptionID");
			entity.Property(e => e.Description)
				.HasMaxLength(400)
				.HasComment("Description of the product.");
			entity.Property(e => e.ModifiedDate)
				.HasComment("Date and time the record was last updated.")
				.HasColumnType("datetime");
			entity.Property(e => e.Rowguid)
				.HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
				.HasColumnName("rowguid");
		});

		modelBuilder.Entity<ProductModel>(entity =>
		{
			entity.HasKey(e => e.ProductModelId).HasName("PK_ProductModel_ProductModelID");

			entity.ToTable("ProductModel", "SalesLT");


			entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
			entity.Property(e => e.CatalogDescription).HasColumnType("xml");
			entity.Property(e => e.ModifiedDate)
				.HasColumnType("datetime");
			entity.Property(e => e.Name).HasMaxLength(50);
			entity.Property(e => e.Rowguid)
				.HasColumnName("rowguid");

			entity.HasIndex(e => e.Name, "AK_ProductModel_Name").IsUnique();

			entity.HasIndex(e => e.Rowguid, "AK_ProductModel_rowguid").IsUnique();

			//TODO: entity.HasIndex(e => e.CatalogDescription, "PXML_ProductModel_CatalogDescription");
		});

		modelBuilder.Entity<ProductModelProductDescription>(entity =>
		{
			entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.Culture }).HasName("PK_ProductModelProductDescription_ProductModelID_ProductDescriptionID_Culture");

			entity.ToTable("ProductModelProductDescription", "SalesLT", tb => tb.HasComment("Cross-reference table mapping product descriptions and the language the description is written in."));

			entity.HasIndex(e => e.Rowguid, "AK_ProductModelProductDescription_rowguid").IsUnique();

			entity.Property(e => e.ProductModelId)
				.HasComment("Primary key. Foreign key to ProductModel.ProductModelID.")
				.HasColumnName("ProductModelID");
			entity.Property(e => e.ProductDescriptionId)
				.HasComment("Primary key. Foreign key to ProductDescription.ProductDescriptionID.")
				.HasColumnName("ProductDescriptionID");
			entity.Property(e => e.Culture)
				.HasMaxLength(6)
				.IsFixedLength()
				.HasComment("The culture for which the description is written");
			entity.Property(e => e.ModifiedDate)
				.HasComment("Date and time the record was last updated.")
				.HasColumnType("datetime");
			entity.Property(e => e.Rowguid)
				.HasColumnName("rowguid");

			entity.HasOne(d => d.ProductDescription).WithMany(p => p.ProductModelProductDescriptions)
				.HasForeignKey(d => d.ProductDescriptionId)
				.OnDelete(DeleteBehavior.ClientSetNull);

			entity.HasOne(d => d.ProductModel).WithMany(p => p.ProductModelProductDescriptions)
				.HasForeignKey(d => d.ProductModelId)
				.OnDelete(DeleteBehavior.ClientSetNull);
		});


		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
