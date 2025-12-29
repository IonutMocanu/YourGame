using System;
using System.Collections.Generic;

namespace APINet.Database.Models;

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Money {get; set;}

    public virtual ICollection<Car>? Cars { get; set; } = new List<Car>();
}