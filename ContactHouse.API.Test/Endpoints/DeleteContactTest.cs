namespace ContactHouse.API.Test.Endpoints;
using ContactHouse.API.Endpoints;
using System.Net;

[TestFixture]
public sealed class DeleteContactTest : ContactEndpointsTest
{
	[Test]
	public async Task GivenEmptyDatabase_WhenDeleteContact_ThenReturnsResponseWithNotFoundStatus()
	{
		var response = await DeleteAsync(1);

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
	}

	[Test]
	public async Task GivenNonEmptyDatabaseAndContactIdThatExists_WhenDeleteContact_ThenReturnsResponseWithNoContent()
	{
		await SeedDatabaseAsync();

		var response = await DeleteAsync(1);

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
	}

	[Test]
	public async Task GivenNonEmptyDatabaseAndContactIdThatDoesNotExist_WhenDeleteContact_ThenReturnsResponseWithNotFoundStatus()
	{
		await SeedDatabaseAsync();

		var response = await DeleteAsync(-1);

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
	}

	private async Task<HttpResponseMessage> DeleteAsync(int contactId)
	{
		using var client = WebApplicationFactory.CreateClient();

		return await client.DeleteAsync($"{ContactEndpoints.Url}/{contactId}");
	}
}