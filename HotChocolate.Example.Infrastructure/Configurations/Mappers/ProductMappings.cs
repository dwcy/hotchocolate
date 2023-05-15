using HotChocolate.Example.Domain.Contracts;
using HotChocolate.Example.Domain.DTO;

namespace HotChocolate.Example.Infrastructure.Configurations.Mappers
{
	/// <summary>
	/// Map product mappers and Projections
	/// </summary>
	public class ProductMappings : Profile
	{
		public ProductMappings()
		{
			AllowNullCollections = true;

			CreateMap<CreateProductRequest, Product>();
			CreateMap<UpdateProductRequest, Product>();
			//CreateMap<ProductDTO, ProductResponse>();

			CreateMap<ProductDTO, Product>();

			CreateProjection<Product, ProductDTO>();
		}
	}
}
