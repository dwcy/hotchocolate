﻿using HotChocolate.Example.Domain.Products.Contracts;
using HotChocolate.Example.Domain.Products.Interfaces;

namespace HotChocolate.Example.Application;

public class ProductService : IProductService
{
	private readonly IProductAdapter _productAdapter;

	public ProductService(IProductAdapter productAdapter)//, IMapper mapper)
	{
		_productAdapter = productAdapter;
	}

	public async Task<ProductResponse> GetProducts(int productId)
	{
		var products = await _productAdapter.GetProductById(productId);
		return new ProductResponse(); // _mapper.Map<ProductResponse>(products);
	}
}
