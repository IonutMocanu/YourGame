namespace APINet.Shared.DataTransferObjects;

public class UserRecord
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int? Money {get; set;}
    public string Name => $"{FirstName} {LastName}";
    public List<CarRecord> Garage { get; set; } = new List<CarRecord>();
}