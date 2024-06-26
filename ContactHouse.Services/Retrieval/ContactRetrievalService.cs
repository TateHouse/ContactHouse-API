namespace ContactHouse.Services.Retrieval;
using AutoMapper;
using ContactHouse.Domain.DTOs;
using ContactHouse.Persistence.Repositories;

public sealed class ContactRetrievalService : IContactRetrievalService
{
	private readonly IContactRepository contactRepository;
	private readonly IMapper mapper;

	public ContactRetrievalService(IContactRepository contactRepository, IMapper mapper)
	{
		this.contactRepository = contactRepository;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<PartialContactDTO>> GetContactsAsync()
	{
		var contacts = (await contactRepository.GetContactsAsync()).ToList();

		return mapper.Map<IEnumerable<PartialContactDTO>>(contacts);
	}
}