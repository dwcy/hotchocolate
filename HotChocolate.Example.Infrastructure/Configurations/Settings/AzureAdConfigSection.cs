namespace HotChocolate.Example.Infrastructure.Configurations.Settings;

public class AzureAdConfigurationSection
{
	public string? Instance { get; set; }
	public string? Domain { get; set; }
	public string? TenantId { get; set; }
	public string? ClientId { get; set; }
	public string? CallbackPath { get; set; }
	public string? SignedOutCallbackPath { get; set; }
	public string? ClientSecret { get; set; }
}
