namespace ContactHouse.API.Test.Endpoints;
using ContactHouse.API.DTOs;
using ContactHouse.API.Endpoints;
using System.Net;
using System.Net.Http.Json;

[TestFixture]
public sealed class GetContactTest : ContactEndpointsTest
{
	[Test]
	public async Task GivenEmptyDatabase_WhenGetContact_ThenReturnsResponseWithNotFoundStatus()
	{
		var response = await GetAsync(1);

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
	}

	[Test]
	public async Task GivenNonEmptyDatabaseAndContactIdThatExists_WhenGetContact_ThenReturnsResponseWithOkStatus()
	{
		await SeedDatabaseAsync();

		var response = await GetAsync(1);

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
	}

	[Test]
	public async Task GivenNonEmptyDatabaseAndContactIdThatExists_WhenGetContact_ThenReturnsResponseContainingContact()
	{
		await SeedDatabaseAsync();

		var response = await GetAsync(1);
		var contact = await response.Content.ReadFromJsonAsync<ResponseContactDTO>();

		Assert.That(contact, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(contact.ContactId, Is.EqualTo(1));
			Assert.That(contact.FirstName, Is.EqualTo("John"));
			Assert.That(contact.LastName, Is.EqualTo("Doe"));
			Assert.That(contact.CompanyName, Is.EqualTo("Doe Incorporated"));
		});
	}

	[Test]
	public async Task GivenNonEmptyDatabaseAndContactIdThatDoesNotExist_WhenGetContact_ThenReturnsResponseWithNotFoundStatus()
	{
		await SeedDatabaseAsync();

		var response = await GetAsync(-1);

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
	}

	private async Task<HttpResponseMessage> GetAsync(int contactId)
	{
		using var client = WebApplicationFactory.CreateClient();

		return await client.GetAsync($"{ContactEndpoints.Url}/{contactId}");
	}
}