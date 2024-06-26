namespace ContactHouse.Persistence.Databases;
using ContactHouse.Domain.Entities;

public interface IContactDatabaseSeeder
{
	public Task SeedDatabaseAsync(IEnumerable<Contact>? contacts = null);
}