namespace ContactHouse.API.Endpoints;
using AutoMapper;
using ContactHouse.API.DTOs;
using ContactHouse.Services.Retrieval;

public static class GetContacts
{
	public async static Task<IResult> HandleAsync(IContactRetrievalService contactRetrievalService, IMapper mapper)
	{
		var contacts = (await contactRetrievalService.GetContactsAsync()).ToList();
		var response = mapper.Map<List<ResponsePartialContactDTO>>(contacts);

		return Results.Ok(response);
	}
}