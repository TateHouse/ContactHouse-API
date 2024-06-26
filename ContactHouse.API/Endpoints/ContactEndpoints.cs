namespace ContactHouse.API.Endpoints;
public static class ContactEndpoints
{
	public const string Url = "/api/v1/contacts";

	public static void ConfigureEndpoints(WebApplication webApplication)
	{
		var endpoint = webApplication.MapGroup(ContactEndpoints.Url);

		endpoint.MapGet("/", GetContacts.HandleAsync);
		endpoint.MapGet("/{contactId}", GetContact.HandleAsync);
	}
}