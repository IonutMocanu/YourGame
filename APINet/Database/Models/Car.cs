using System.Reflection.Metadata;

namespace APINet.Database.Models;

public class Car
{
    public int Id {get; set;}
    public string? Manufacturer {get; set;}
    public string? Model {get; set;}
    public int Year {get; set;}
    public float Speed {get; set;}
    public float Price {get; set;}
    public byte[]? ObjModel {get; set;} //modelul 3d
    public int UserId {get; set;} // cheia de legatura
    public User? User{get; set;}
}