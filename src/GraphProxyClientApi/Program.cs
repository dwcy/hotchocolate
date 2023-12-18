using BackendServices.GraphClients;
using StrawberryShake;

namespace GraphProxyClientApi;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services
		.AddGraphProxyClientApi()
		.ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:5000/graphql"));

		var app = builder.Build();

		app.MapGet("/product/{id}",  async (int id) =>
		{
			var client = app.Services.GetRequiredService<IGraphProxyClientApi>();

			var result = await client.GetProductById.ExecuteAsync(id);
			result.EnsureNoErrors();

			return result.Data.Product;
		});

		await app.RunAsync();
	}
}
