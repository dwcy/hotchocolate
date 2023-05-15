using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace HotChocolate.Example.Tests.Base.TestAttributes
{
	/// <summary>
	/// This property is used only to resolve a generic class property for GraphQLResponse
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class GenericJsonPropertyAttribute : Attribute { }

	/// <summary>
	/// This is a custom Json Serializer Setting to use when you want to resolve generic class properties using 
	/// the attribute of type GenericJsonPropertyAttribute
	/// </summary>
	public class GenericJsonPropertyResolver : CamelCasePropertyNamesContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var prop = base.CreateProperty(member, memberSerialization);

			var hasAttribute = member.GetCustomAttribute<GenericJsonPropertyAttribute>();
			if (hasAttribute != null)
			{
				var typeArgs = member?.DeclaringType?.GetGenericArguments();
				prop.PropertyName = typeArgs![0].Name;
			}

			return prop;
		}
	}
}
