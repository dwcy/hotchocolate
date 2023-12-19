namespace HotChocolate.Example.Domain.Products.Contracts;

public class ProductUpdateRequest
{
	public int ProductId { get; set; }
	public string Name { get; set; }
	public int? ProductNumber { get; set; }
}
