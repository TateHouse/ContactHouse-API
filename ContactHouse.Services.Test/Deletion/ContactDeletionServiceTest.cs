namespace ContactHouse.Services.Test.Deletion;
using AutoMapper;
using ContactHouse.Domain.Entities;
using ContactHouse.Domain.Profiles;
using ContactHouse.Persistence.Repositories;
using ContactHouse.Services.Deletion;
using Moq;

[TestFixture]
public sealed class ContactDeletionServiceTest
{
	private Mock<IContactRepository> mockContactRepository;
	private ContactDeletionService contactDeletionService;

	[SetUp]
	public void SetUp()
	{
		mockContactRepository = new Mock<IContactRepository>();
		contactDeletionService = new ContactDeletionService(mockContactRepository.Object);
	}

	[Test]
	public async Task GivenContactDatabaseAndContactIdThatDoesNotExist_WhenDeleteContactAsync_ThenReturnsFalse()
	{
		mockContactRepository.Setup(mock => mock.DeleteContactAsync(It.IsAny<int>()))
							 .ReturnsAsync(false);

		var wasDeleted = await contactDeletionService.DeleteContactAsync(1);

		Assert.That(wasDeleted, Is.False);
	}

	[Test]
	public async Task GivenContactDatabaseAndContactIdThatExists_WhenDeleteContactAsync_ThenReturnsTrue()
	{

		mockContactRepository.Setup(mock => mock.DeleteContactAsync(1))
							 .ReturnsAsync(true);

		var wasDeleted = await contactDeletionService.DeleteContactAsync(1);

		Assert.That(wasDeleted, Is.True);
	}
}