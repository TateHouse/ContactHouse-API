﻿namespace ContactHouse.Persistence.Databases;
using ContactHouse.Domain.Entities;

public sealed class ContactDatabaseSeeder : IContactDatabaseSeeder
{
	private readonly ContactDatabaseContext databaseContext;
	private readonly static List<Contact> DefaultContacts = new List<Contact>
	{
		new Contact
		{
			ContactId = 1,
			FirstName = "John",
			LastName = "Doe",
			CompanyName = "Doe Incorporated"
		},
		new Contact
		{
			ContactId = 2,
			FirstName = "Bob",
		},
		new Contact
		{
			ContactId = 3,
			FirstName = "Samantha",
			LastName = "Smith"
		},
		new Contact
		{
			ContactId = 4,
			FirstName = "Clark",
			CompanyName = "Clark's Cars"
		},
	};

	public ContactDatabaseSeeder(ContactDatabaseContext databaseContext)
	{
		this.databaseContext = databaseContext;
	}

	public async Task SeedDatabaseAsync(IEnumerable<Contact>? contacts = null)
	{
		contacts ??= ContactDatabaseSeeder.DefaultContacts;

		await databaseContext.Contacts.AddRangeAsync(contacts);
		await databaseContext.SaveChangesAsync();
	}
}