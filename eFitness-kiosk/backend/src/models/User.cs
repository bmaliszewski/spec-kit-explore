namespace eFitnessKiosk.Models;

public class User
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public object PersonalData { get; set; }
    public object[] RodoConsents { get; set; }
}
