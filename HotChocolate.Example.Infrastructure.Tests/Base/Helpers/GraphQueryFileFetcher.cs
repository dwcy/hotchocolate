namespace HotChocolate.Example.Tests.Base.Helpers
{
	public enum TestGraphQuery
	{
		CreateProductMutation,
		DeleteProductMutation,
		UpdateProductMutation,
		ProductQuery
	}

	public class GraphQueryFileFetcher
	{
		/// <summary>
		/// As default it get filename and attach .json and try to find it in the root of Test Project in folder "ProductTool/TestData/"
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="defaultFolder"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task<string> GetGraphQuery(TestGraphQuery fileName, string defaultFolder = "Base/TestGraphQueries/")
		{
			var basePath = Directory.GetCurrentDirectory();
			string fullPath = System.IO.Path.Combine(basePath, defaultFolder, $"{fileName}.graphql");

			if (!File.Exists(fullPath))
				throw new ArgumentException($"Could not find file at path: {fullPath}");

			var fileData = await File.ReadAllTextAsync(fullPath);

			return fileData;
		}
	}
}
