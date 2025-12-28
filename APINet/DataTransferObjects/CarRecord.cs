namespace APINet.DataTransferObjects;

public class CarRecord
{
    public int Id { get; set; }
    public string Manufacturer { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public float Speed { get; set; }
    public float Price { get; set; }
    public string FullName => $"{Manufacturer} {Model} ({Year})";
    public string? OwnerName { get; set; } 
}