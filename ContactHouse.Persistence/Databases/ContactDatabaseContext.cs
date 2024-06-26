namespace ContactHouse.Persistence.Databases;
using ContactHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ContactDatabaseContext : DbContext
{
	public virtual DbSet<Contact> Contacts { get; set; }

	public ContactDatabaseContext(DbContextOptions<ContactDatabaseContext> databaseContextOptions)
		: base(databaseContextOptions)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		var entityBuilder = modelBuilder.Entity<Contact>();
		entityBuilder.HasKey(entity => entity.ContactId);
	}
}