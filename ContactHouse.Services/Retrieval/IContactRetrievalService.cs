namespace ContactHouse.Services.Retrieval;
using ContactHouse.Domain.DTOs;

public interface IContactRetrievalService
{
	public Task<IEnumerable<PartialContactDTO>> GetContactsAsync();
}