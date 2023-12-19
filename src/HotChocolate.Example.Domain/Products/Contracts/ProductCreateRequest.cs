namespace HotChocolate.Example.Domain.Products.Contracts;

public class ProductCreateRequest
{
	public string Name { get; set; } = null!;

	public int ProductNumber { get; set; }
}