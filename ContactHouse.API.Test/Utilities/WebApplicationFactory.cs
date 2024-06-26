namespace ContactHouse.API.Test.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

public sealed class WebApplicationFactory : WebApplicationFactory<Program>
{
	protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
	{
		webHostBuilder.ConfigureAppConfiguration((context, configuation) =>
		{
			configuation.AddJsonFile("API.AppSettings.Test.json", false, true);
		});
	}
}