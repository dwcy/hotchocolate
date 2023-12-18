using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace HotChocolate.Example.Infrastructure.Configurations.Logging;
public static class LoggerBuilderExtensions
{
	public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
	{
		builder.Logging.AddConsole();
		builder.Logging.AddDebug();
		builder.Logging.AddEventSourceLogger();

		return builder;
	}
}
