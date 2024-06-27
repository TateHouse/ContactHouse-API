namespace ContactHouse.Services.Deletion;
public interface IContactDeletionService
{
	public Task<bool> DeleteContactAsync(int contactId);
}