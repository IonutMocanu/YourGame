namespace APINet.Shared.DataTransferObjects;

public class UserAddRecord
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Money {get; set;} = 100;
}