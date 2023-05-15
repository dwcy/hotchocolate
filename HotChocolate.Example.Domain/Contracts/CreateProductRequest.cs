namespace HotChocolate.Example.Domain.Contracts;
public class CreateProductRequest
{
	public int ProductId { get; set; }

	public string Name { get; set; } = null!;
}
