namespace APINet.Shared.DataTransferObjects;

public class CarAddRecord
{
    public string Manufacturer { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public float Speed { get; set; }
    public float Price { get; set; }
    public int UserId { get; set; } 
}