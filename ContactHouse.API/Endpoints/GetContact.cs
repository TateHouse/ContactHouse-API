namespace ContactHouse.API.Endpoints;
using AutoMapper;
using ContactHouse.API.DTOs;
using ContactHouse.Services.Retrieval;

public static class GetContact
{
	public async static Task<IResult> HandleAsync(IContactRetrievalService contactRetrievalService,
												  IMapper mapper,
												  int contactId)
	{
		var contact = await contactRetrievalService.GetContactAsync(contactId);

		if (contact == null)
		{
			return Results.NotFound();
		}

		var response = mapper.Map<ResponseContactDTO>(contact);

		return Results.Ok(response);
	}
}