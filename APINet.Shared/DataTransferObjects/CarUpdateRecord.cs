namespace APINet.Shared.DataTransferObjects;

public class CarUpdateRecord
{
    public int Id { get; set; } 
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public float? Speed { get; set; }
    public float? Price { get; set; }
}