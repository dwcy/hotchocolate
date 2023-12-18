using HotChocolate.Example.Domain.Products.Contracts;
using HotChocolate.Example.Domain.Products.DTO;

namespace HotChocolate.Example.Infrastructure.Configurations.Mappers;

/// <summary>
/// Map product mappers and Projections
/// </summary>
public class ProductMappings : Profile
{
	public ProductMappings()
	{
		AllowNullCollections = true;

		CreateMap<ProductCreateRequest, Product>();
		CreateMap<ProductUpdateRequest, Product>();
		CreateMap<ProductDto, ProductResponse>();
		CreateMap<Product, ProductResponse>();
		CreateMap<ProductDto, Product>();

		CreateProjection<Product, ProductDto>();
	}
}
