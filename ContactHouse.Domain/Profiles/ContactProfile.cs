namespace ContactHouse.Domain.Profiles;
using AutoMapper;
using ContactHouse.Domain.DTOs;
using ContactHouse.Domain.Entities;

public sealed class ContactProfile : Profile
{
	public ContactProfile()
	{
		CreateMap<Contact, ContactDTO>();
		CreateMap<Contact, PartialContactDTO>();
	}
}