namespace HotChocolate.Example.Domain.Entities;

public partial class VGetAllCategory
{
	public string ParentProductCategoryName { get; set; } = null!;

	public string? ProductCategoryName { get; set; }

	public int? ProductCategoryId { get; set; }
}
