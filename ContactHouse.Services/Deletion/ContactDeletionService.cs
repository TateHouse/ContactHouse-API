namespace ContactHouse.Services.Deletion;
using ContactHouse.Persistence.Repositories;

public class ContactDeletionService : IContactDeletionService
{
	private readonly IContactRepository contactRepository;

	public ContactDeletionService(IContactRepository contactRepository)
	{
		this.contactRepository = contactRepository;
	}

	public async Task<bool> DeleteContactAsync(int contactId)
	{
		return await contactRepository.DeleteContactAsync(contactId);
	}
}