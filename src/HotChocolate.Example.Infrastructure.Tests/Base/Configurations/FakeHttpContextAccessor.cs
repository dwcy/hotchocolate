using Microsoft.AspNetCore.Http;

namespace HotChocolate.Example.Tests.Base.Configurations;

public class FakeContextAccessor : HttpContextAccessor
{
	public FakeContextAccessor()
	{
		HttpContext = new DefaultHttpContext();
		HttpContext.User = CreateFakePrincipal();
	}

	public static ClaimsPrincipal CreateFakePrincipal()
	{
		var claims = new List<Claim> {
			new Claim(ClaimTypes.Name, "Fake User"),
			new Claim(ClaimTypes.NameIdentifier, "FakeUser"),
			new Claim(ClaimTypes.Role, "Create"),
			new Claim(ClaimTypes.Role, "Read"),
			new Claim(ClaimTypes.Role, "Update"),
			new Claim(ClaimTypes.Role, "Delete"),
		};

		var identity = new ClaimsIdentity(claims, "TestAuthType");
		var principal = new ClaimsPrincipal(identity);

		return principal;
	}
}
