using HotChocolate.Example.Domain.Products.DTO;

namespace HotChocolate.Example.Domain.Products.Contracts;

public record ProductResponse
{
	public int ProductId { get; set; }
	public int ProductNumber { get; set; }
	public string Name { get; set; }
}
