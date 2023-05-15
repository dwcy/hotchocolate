using HotChocolate.Example.Domain.DTO;

namespace HotChocolate.Example.Tests.Base.Models;

/// <summary>
/// it is used as Graphql response model
/// </summary>
#pragma warning disable CA1724
public class Products
{
	public int TotalCount { get; set; }
	public List<ProductDTO>? Nodes { get; set; }
}
#pragma warning restore CA1724
