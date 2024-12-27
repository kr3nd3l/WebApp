

namespace API.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
public class Tour
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; }
}
