namespace ContactHouse.Persistence.Test.Repositories;
using ContactHouse.Domain.Entities;
using ContactHouse.Persistence.Databases;
using ContactHouse.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

[TestFixture]
public sealed class ContactRepositoryTest
{
	private ContactDatabaseContext contactDatabaseContext;
	private ContactRepository contactRepository;

	private readonly List<Contact> databaseContacts = new List<Contact>
	{
		new Contact
		{
			ContactId = 1
		},
		new Contact
		{
			ContactId = 2
		}
	};

	[SetUp]
	public void SetUp()
	{
		var contactDatabaseContextOptions = new DbContextOptionsBuilder<ContactDatabaseContext>();
		contactDatabaseContextOptions.UseInMemoryDatabase("ContactHouse.Persistence In-Memory Testing Database");

		contactDatabaseContext = new ContactDatabaseContext(contactDatabaseContextOptions.Options);
		contactDatabaseContext.Database.EnsureDeleted();
		contactDatabaseContext.Database.EnsureCreated();

		contactRepository = new ContactRepository(contactDatabaseContext);
	}

	[TearDown]
	public void TearDown()
	{
		contactDatabaseContext.Dispose();
	}

	[Test]
	public async Task GivenEmptyDatabaseContext_WhenGetContactsAsync_ThenReturnsNoContacts()
	{
		var contacts = (await contactRepository.GetContactsAsync()).ToList();

		Assert.That(contacts, Is.Empty);
	}

	[Test]
	public async Task GivenNonEmptyDatabaseContext_WhenGetContactsAsync_ThenReturnsContacts()
	{
		await SeedDatabaseAsync();

		var contacts = (await contactRepository.GetContactsAsync()).ToList();

		Assert.That(contacts, Has.Exactly(2).Items);
	}

	private async Task SeedDatabaseAsync()
	{
		await contactDatabaseContext.Contacts.AddRangeAsync(databaseContacts);
		await contactDatabaseContext.SaveChangesAsync();
	}
}