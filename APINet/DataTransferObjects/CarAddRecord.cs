namespace APINet.DataTransferObjects;

public class CarAddRecord
{
    public string Manufacturer { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public float Speed { get; set; }
    public float Price { get; set; }
    
    // Dacă vrei să poți atribui mașina direct la creare, poți lăsa asta. 
    // Dacă o iei din URL (ex: /buy/{userId}), poți șterge linia asta.
    public int UserId { get; set; } 
}