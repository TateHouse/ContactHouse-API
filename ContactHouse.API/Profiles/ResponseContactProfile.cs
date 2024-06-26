namespace ContactHouse.API.Profiles;
using AutoMapper;
using ContactHouse.API.DTOs;
using ContactHouse.Domain.DTOs;

public sealed class ResponseContactProfile : Profile
{
	public ResponseContactProfile()
	{
		CreateMap<ContactDTO, ResponseContactDTO>();
		CreateMap<PartialContactDTO, ResponsePartialContactDTO>();
	}
}