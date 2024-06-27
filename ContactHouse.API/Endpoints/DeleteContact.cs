namespace ContactHouse.API.Endpoints;
using ContactHouse.Services.Deletion;

public static class DeleteContact
{
	public async static Task<IResult> HandleAsync(IContactDeletionService contactDeletionService, int contactId)
	{
		var wasDeleted = await contactDeletionService.DeleteContactAsync(contactId);

		return wasDeleted ? Results.NoContent() : Results.NotFound();
	}
}