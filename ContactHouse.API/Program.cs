namespace ContactHouse.API;
using ContactHouse.API.Endpoints;
using ContactHouse.API.Profiles;
using ContactHouse.Domain.Profiles;
using ContactHouse.Persistence.Databases;
using ContactHouse.Persistence.Repositories;
using ContactHouse.Services.Deletion;
using ContactHouse.Services.Retrieval;
using Microsoft.EntityFrameworkCore;

public class Program
{
	public async static Task Main(string[] args)
	{
		var webApplicationBuilder = WebApplication.CreateBuilder(args);

		Program.SetUpConfigurationFiles(webApplicationBuilder);
		Program.RegisterAutoMappers(webApplicationBuilder);
		Program.RegisterContactServices(webApplicationBuilder);
		Program.RegisterRepositories(webApplicationBuilder);
		Program.SetUpDatabaseContext(webApplicationBuilder);

		var application = webApplicationBuilder.Build();

		if (application.Environment.IsDevelopment())
		{
			application.UseDeveloperExceptionPage();

			var shouldSeedDatabase = webApplicationBuilder.Configuration.GetValue<bool>("SeedDatabase");

			if (shouldSeedDatabase)
			{
				using var scope = application.Services.CreateScope();
				var contactDatabaseSeeder = scope.ServiceProvider.GetRequiredService<IContactDatabaseSeeder>();
				await contactDatabaseSeeder.SeedDatabaseAsync();
			}
		}

		ContactEndpoints.ConfigureEndpoints(application);

		await application.RunAsync();
	}

	private static void SetUpConfigurationFiles(WebApplicationBuilder webApplicationBuilder)
	{
		webApplicationBuilder.Configuration.Sources.Clear();
		webApplicationBuilder.Configuration.AddJsonFile("API.AppSettings.json", false, true);
		webApplicationBuilder.Configuration.AddJsonFile($"API.AppSettings.{webApplicationBuilder.Environment.EnvironmentName}.json", true, true);
		webApplicationBuilder.Configuration.AddEnvironmentVariables();
	}

	private static void RegisterAutoMappers(WebApplicationBuilder webApplicationBuilder)
	{
		webApplicationBuilder.Services.AddAutoMapper(typeof(ContactProfile).Assembly);
		webApplicationBuilder.Services.AddAutoMapper(typeof(ResponseContactProfile).Assembly);
	}

	private static void RegisterContactServices(WebApplicationBuilder webApplicationBuilder)
	{
		webApplicationBuilder.Services.AddScoped<IContactRetrievalService, ContactRetrievalService>();
		webApplicationBuilder.Services.AddScoped<IContactDeletionService, ContactDeletionService>();
	}

	private static void RegisterRepositories(WebApplicationBuilder webApplicationBuilder)
	{
		webApplicationBuilder.Services.AddScoped<IContactRepository, ContactRepository>();
	}

	private static void SetUpDatabaseContext(WebApplicationBuilder webApplicationBuilder)
	{
		webApplicationBuilder.Services.AddDbContext<ContactDatabaseContext>(options =>
		{
			options.UseInMemoryDatabase("ContactHouse-API In-Memory Database");
		});

		webApplicationBuilder.Services.AddScoped<IContactDatabaseSeeder, ContactDatabaseSeeder>();
	}
}