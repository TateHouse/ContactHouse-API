﻿namespace ContactHouse.Persistence.Test.Repositories;
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
			ContactId = 1,
			FirstName = "John",
		},
		new Contact
		{
			ContactId = 2,
			FirstName = "Bob"
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
	public async Task GivenEmptyDatabaseContext_WhenGetContactsAsync_ThenReturnsEmptyEnumerable()
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

	[Test]
	public async Task GivenEmptyDatabaseContext_WhenGetContactAsync_ThenReturnsNull()
	{
		var contact = await contactRepository.GetContactAsync(1);

		Assert.That(contact, Is.Null);
	}

	[Test]
	public async Task GivenNonEmptyDatabaseContextAndContactIdThatExists_WhenGetContactAsync_ThenReturnsContact()
	{
		await SeedDatabaseAsync();

		var contact = await contactRepository.GetContactAsync(1);

		Assert.That(contact, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(contact.ContactId, Is.EqualTo(1));
			Assert.That(contact.FirstName, Is.EqualTo("John"));
		});
	}

	[Test]
	public async Task GivenNonEmptyDatabaseContextAndContactIdThatDoesNotExist_WhenGetContactAsync_ThenReturnsNull()
	{
		await SeedDatabaseAsync();

		var contact = await contactRepository.GetContactAsync(-1);

		Assert.That(contact, Is.Null);
	}

	[Test]
	public async Task GivenEmptyDatabaseContext_WhenDeleteContactAsync_ThenReturnsFalse()
	{
		var wasDeleted = await contactRepository.DeleteContactAsync(1);

		Assert.That(wasDeleted, Is.False);
	}

	[Test]
	public async Task GivenNonEmptyDatabaseContextAndContactIdThatExists_WhenDeleteContactAsync_ThenReturnsTrue()
	{
		await SeedDatabaseAsync();

		var wasDeleted = await contactRepository.DeleteContactAsync(1);

		Assert.That(wasDeleted, Is.True);
	}

	[Test]
	public async Task GivenNonEmptyDatabaseContextAndContactIdThatDoesNotExist_WhenDeleteContactAsync_ThenReturnsFalse()
	{
		await SeedDatabaseAsync();

		var wasDeleted = await contactRepository.DeleteContactAsync(-1);

		Assert.That(wasDeleted, Is.False);
	}

	private async Task SeedDatabaseAsync()
	{
		await contactDatabaseContext.Contacts.AddRangeAsync(databaseContacts);
		await contactDatabaseContext.SaveChangesAsync();
	}
}