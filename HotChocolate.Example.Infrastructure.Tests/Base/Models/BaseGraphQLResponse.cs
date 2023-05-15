using HotChocolate.Example.Tests.Base.TestAttributes;

namespace HotChocolate.Example.Tests.Base.Models
{
	/// <summary>
	/// Root of the graphQL response
	/// </summary>
	public class GraphqlResponse<T>
	{
		public GraphqlData<T>? Data { get; set; }
		public List<GraphqlError>? Errors { get; set; }
	}

	/// <summary>
	/// Contains your result of requested type T
	/// </summary>
	/// <typeparam name="T">Your generic response object</typeparam>
	public class GraphqlData<T>
	{
		[GenericJsonProperty]
		public T? Result { get; set; }
	}

	/// <summary>
	/// GraphQL Error message bucket
	/// </summary>
	public class GraphqlError
	{
		public string? Message { get; set; }
		public GraphqlExtensions? Extensions { get; set; }
	}

	/// <summary>
	/// GraphQL Error message extensions
	/// </summary>
	public class GraphqlExtensions
	{
		public string? Code { get; set; }
	}
}
