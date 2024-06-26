namespace ContactHouse.API.Test.Endpoints;
using ContactHouse.API.DTOs;
using ContactHouse.API.Endpoints;
using System.Net;
using System.Net.Http.Json;

[TestFixture]
public sealed class GetContactsTest : ContactEndpointsTest
{
	[Test]
	public async Task GivenEmptyDatabase_WhenGetContacts_ThenReturnsResponseWithOkStatus()
	{
		var response = await GetAsync();

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
	}

	[Test]
	public async Task GivenEmptyDatabase_WhenGetContacts_ThenReturnsResponseContainingZeroContacts()
	{
		var response = await GetAsync();
		var contacts = await response.Content.ReadFromJsonAsync<List<ResponsePartialContactDTO>>();

		Assert.That(contacts, Is.Empty);
	}

	[Test]
	public async Task GivenNonEmptyDatabase_WhenGetContacts_ThenReturnsResponseWithOkStatus()
	{
		await SeedDatabaseAsync();

		var response = await GetAsync();

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
	}

	[Test]
	public async Task GivenNonEmptyDatabase_WhenGetContacts_ThenReturnsResponseContainingFourContacts()
	{
		await SeedDatabaseAsync();

		var response = await GetAsync();
		var contacts = await response.Content.ReadFromJsonAsync<List<ResponsePartialContactDTO>>();

		Assert.That(contacts, Has.Exactly(4).Items);
	}

	private async Task<HttpResponseMessage> GetAsync()
	{
		using var client = WebApplicationFactory.CreateClient();

		return await client.GetAsync(ContactEndpoints.Url);
	}
}