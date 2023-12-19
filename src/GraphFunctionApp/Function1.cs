using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Serialization;
using HotChocolate.AspNetCore.Serialization;
using HotChocolate.Example.Infrastructure.Configurations.DependencyInjection;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GraphFunctionApp
{
	public static class Function1
	{
		private readonly IHttpRequestParser _requestParser;
		private readonly IHttpResultSerializer _resultSerializer;
		private readonly IRequestExecutorResolver _requestExecutorResolver;

		public Function1(IHttpRequestParser requestParser, IRequestExecutorResolver requestExecutorResolver)
		{
			_requestParser = requestParser;
		}

		[FunctionName("GraphQLFunction")]
		public static async Task<IActionResult> Run(
		[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
		{
			var config = new ConfigurationBuilder()
		   .SetBasePath(context.FunctionAppDirectory)
		   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
		   .AddEnvironmentVariables()
		   .Build();

			var services = new ServiceCollection()
				.AddInfrastructureDependencies()
			   .Services
			   .BuildServiceProvider();

			var server = services.GetService<ISchema>();

			// Enable Playground and Voyager in development environment
			if (Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development")
			{
				app.UsePlayground();
				app.UseVoyager();
			}

			return await req.ReadResponseAsync(ctx => ctx.UseSchema(server))
				.ConfigureAwait(false);
		}
	}
}
}
