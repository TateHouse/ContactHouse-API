﻿namespace ContactHouse.Persistence.Repositories;
using ContactHouse.Domain.Entities;

public interface IContactRepository
{
	public Task<IEnumerable<Contact>> GetContactsAsync();
}