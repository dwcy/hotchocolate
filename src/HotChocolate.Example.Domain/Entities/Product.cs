﻿namespace HotChocolate.Example.Domain.Entities;

/// <summary>
/// Products sold or used in the manfacturing of sold products.
/// </summary>
public class Product
{
	/// <summary>
	/// Primary key for Product records.
	/// </summary>
	public int ProductId { get; set; }

	/// <summary>
	/// Name of the product.
	/// </summary>
	public string Name { get; set; } = null!;

	/// <summary>
	/// Unique product identification number.
	/// </summary>
	public string ProductNumber { get; set; } = null!;

	/// <summary>
	/// Product color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Standard cost of the product.
	/// </summary>
	public decimal StandardCost { get; set; }

	/// <summary>
	/// Selling price.
	/// </summary>
	public decimal ListPrice { get; set; }

	/// <summary>
	/// Product size.
	/// </summary>
	public string? Size { get; set; }

	/// <summary>
	/// Product weight.
	/// </summary>
	public decimal? Weight { get; set; }

	/// <summary>
	/// Product is a member of this product category. Foreign key to ProductCategory.ProductCategoryID. 
	/// </summary>
	public int? ProductCategoryId { get; set; }

	/// <summary>
	/// Product is a member of this product model. Foreign key to ProductModel.ProductModelID.
	/// </summary>
	public int? ProductModelId { get; set; }

	/// <summary>
	/// Date the product was available for sale.
	/// </summary>
	public DateTime SellStartDate { get; set; } = DateTime.UtcNow.AddDays(30);

	/// <summary>
	/// Date the product was no longer available for sale.
	/// </summary>
	public DateTime? SellEndDate { get; set; }

	/// <summary>
	/// Date the product was discontinued.
	/// </summary>
	public DateTime? DiscontinuedDate { get; set; }

	/// <summary>
	/// Small image of the product.
	/// </summary>
#pragma warning disable CA1819
	public byte[]? ThumbNailPhoto { get; set; }
#pragma warning restore CA1819

	/// <summary>
	/// Small image file name.
	/// </summary>
	public string? ThumbnailPhotoFileName { get; set; }


	/// <summary>
	/// Date and time the record was last updated.
	/// </summary>
	public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

	public virtual ProductCategory? ProductCategory { get; set; }

	public virtual ProductModel? ProductModel { get; set; }
}
