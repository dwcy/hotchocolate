using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace HotChocolate.Example.Tests.Base.TestAttributes
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class GenericTestCaseAttribute : TestCaseAttribute, ITestBuilder
	{
		private readonly Type _type;

		public GenericTestCaseAttribute(Type type, params object[] arguments) : base(arguments)
		{
			_type = type;
		}

		IEnumerable<TestMethod> ITestBuilder.BuildFrom(IMethodInfo method, Test? suite)
		{
			if (method.IsGenericMethodDefinition && _type != null)
			{
				var generatedMethod = method.MakeGenericMethod(_type);
				return BuildFrom(generatedMethod, suite);
			}

			return BuildFrom(method, suite);
		}
	}
}
