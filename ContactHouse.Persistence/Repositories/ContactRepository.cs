namespace ContactHouse.Persistence.Repositories;
using ContactHouse.Domain.Entities;
using ContactHouse.Persistence.Databases;
using Microsoft.EntityFrameworkCore;

public sealed class ContactRepository : IContactRepository
{
	private readonly ContactDatabaseContext contactDatabaseContext;

	public ContactRepository(ContactDatabaseContext contactDatabaseContext)
	{
		this.contactDatabaseContext = contactDatabaseContext;
	}

	public async Task<IEnumerable<Contact>> GetContactsAsync()
	{
		return await contactDatabaseContext.Contacts.ToListAsync();
	}

	public async Task<Contact?> GetContactAsync(int contactId)
	{
		return await contactDatabaseContext.Contacts.FirstOrDefaultAsync(contact => contact.ContactId == contactId);
	}
}