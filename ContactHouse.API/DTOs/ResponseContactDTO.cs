namespace ContactHouse.API.DTOs;
public class ResponseContactDTO
{
	public int ContactId { get; set; }
	public string FirstName { get; set; }
	public string? LastName { get; set; }
	public string? CompanyName { get; set; }
}