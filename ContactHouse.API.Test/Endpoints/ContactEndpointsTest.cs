namespace ContactHouse.API.Test.Endpoints;
using ContactHouse.API.Test.Utilities;
using ContactHouse.Persistence.Databases;
using Microsoft.Extensions.DependencyInjection;

public abstract class ContactEndpointsTest
{
	protected WebApplicationFactory WebApplicationFactory { get; private set; }

	[SetUp]
	public void SetUp()
	{
		WebApplicationFactory = new WebApplicationFactory();

		using var scope = WebApplicationFactory.Services.CreateScope();
		var contactDatabaseContext = scope.ServiceProvider.GetRequiredService<ContactDatabaseContext>();
		contactDatabaseContext.Database.EnsureDeleted();
		contactDatabaseContext.Database.EnsureCreated();
	}

	[TearDown]
	public void TearDown()
	{
		WebApplicationFactory.Dispose();
	}

	protected async Task SeedDatabaseAsync()
	{
		using var scope = WebApplicationFactory.Services.CreateScope();
		var contactDatabaseSeeder = scope.ServiceProvider.GetRequiredService<IContactDatabaseSeeder>();
		await contactDatabaseSeeder.SeedDatabaseAsync();
	}
}