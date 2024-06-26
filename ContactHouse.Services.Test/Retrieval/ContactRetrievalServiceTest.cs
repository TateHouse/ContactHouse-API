﻿namespace ContactHouse.Services.Test.Retrieval;
using AutoMapper;
using ContactHouse.Domain.Entities;
using ContactHouse.Domain.Profiles;
using ContactHouse.Persistence.Repositories;
using ContactHouse.Services.Retrieval;
using Moq;

[TestFixture]
public sealed class ContactRetrievalServiceTest
{
	private Mock<IContactRepository> mockContactRepository;
	private ContactRetrievalService contactRetrievalService;

	private readonly List<Contact> databaseContacts = new List<Contact>
	{
		new Contact
		{
			ContactId = 1
		},
		new Contact
		{
			ContactId = 2
		}
	};

	[SetUp]
	public void SetUp()
	{
		mockContactRepository = new Mock<IContactRepository>();

		var mapperConfiguration = new MapperConfiguration(configure => configure.AddProfile<ContactProfile>());
		var mapper = mapperConfiguration.CreateMapper();

		contactRetrievalService = new ContactRetrievalService(mockContactRepository.Object, mapper);
	}

	[Test]
	public async Task GivenEmptyDatabase_WhenGetContactsAsync_ThenReturnsNoContacts()
	{
		mockContactRepository.Setup(mock => mock.GetContactsAsync())
							 .ReturnsAsync(new List<Contact>());

		var contacts = (await contactRetrievalService.GetContactsAsync()).ToList();

		Assert.That(contacts, Is.Empty);
	}

	[Test]
	public async Task GivenNonEmptyDatabase_WhenGetContactsAsync_ThenReturnsContacts()
	{
		mockContactRepository.Setup(mock => mock.GetContactsAsync())
							 .ReturnsAsync(databaseContacts);

		var contacts = (await contactRetrievalService.GetContactsAsync()).ToList();

		Assert.That(contacts, Has.Exactly(2).Items);
	}
}