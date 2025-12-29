using Microsoft.EntityFrameworkCore;
using APINet.Database;
using APINet.Database.Models;
using APINet.DataTransferObjects;
using APINet.Services.Abstractions;

namespace APINet.Services.Implementations;

public class UserService(GameDatabaseContext context) : IUserService
{
public async Task<PagedResponse<UserRecord>> GetUsers(SearchPaginationQueryParams queryParams)
{
    var query = context.Users.AsQueryable();

    // 1. Filtrare
    if (!string.IsNullOrWhiteSpace(queryParams.Search))
    {
        var search = queryParams.Search.Trim().ToLower();
        query = query.Where(u => u.Email.ToLower().Contains(search) || u.LastName.ToLower().Contains(search));
    }

    // 2. Numărare totală
    var totalCount = await query.CountAsync();

    // 3. Extragere ENTITĂȚI (Raw Data)
    // Aici NU facem new UserRecord, ci aducem obiectele User din bază
    var userEntities = await query
        .Include(u => u.Cars) // <--- IMPORTANT: Aducem și mașinile
        .OrderBy(u => u.Id)
        .Skip((queryParams.Page - 1) * queryParams.PageSize)
        .Take(queryParams.PageSize)
        .ToListAsync(); // <--- Aici se execută SQL-ul simplu și datele ajung în RAM

    // 4. Mapare în Memorie (Mapping)
    // Acum că avem datele în RAM, C# poate face transformarea complexă fără erori
    var data = userEntities.Select(u => new UserRecord
    {
        Id = u.Id,
        FirstName = u.FirstName,
        LastName = u.LastName,
        Email = u.Email,
        Money = u.Money, 
        
        Garage = u.Cars.Select(c => new CarRecord 
        {
            Id = c.Id,
            Manufacturer = c.Manufacturer ?? "",
            Model = c.Model ?? "",
            Year = c.Year,
            Speed = c.Speed,
            Price = c.Price,
            OwnerName = $"{u.FirstName} {u.LastName}"
        }).ToList()
    }).ToList();

    return new PagedResponse<UserRecord>
    {
        Page = queryParams.Page,
        PageSize = queryParams.PageSize,
        TotalCount = totalCount,
        Data = data
    };
}

    public async Task<UserRecord?> GetUser(int userId)
{
    // PASUL 1: Aducem datele brute din baza de date (fără Select complex)
    var userEntity = await context.Users
        .Include(u => u.Cars) // <--- Aducem mașinile folosind Include
        .FirstOrDefaultAsync(u => u.Id == userId);

    // Verificăm dacă userul există
    if (userEntity == null) 
    {
        return null;
    }

    // PASUL 2: Facem maparea (transformarea) în memorie (C#)
    // Aici nu mai contează că e SQLite, pentru că datele sunt deja la noi.
    var userDto = new UserRecord
    {
        Id = userEntity.Id,
        FirstName = userEntity.FirstName,
        LastName = userEntity.LastName,
        Email = userEntity.Email,
        Money = userEntity.Money,
        
        // Mapăm lista de mașini manual
        Garage = userEntity.Cars.Select(c => new CarRecord
        {
            Id = c.Id,
            Manufacturer = c.Manufacturer ?? "",
            Model = c.Model ?? "",
            Year = c.Year,
            Speed = c.Speed,
            Price = c.Price,
            OwnerName = $"{userEntity.FirstName} {userEntity.LastName}"
        }).ToList()
    };

    return userDto;
}

    public async Task AddUser(UserAddRecord userDto)
    {
        var entity = new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email
        };

        await context.Users.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUser(UserUpdateRecord userDto)
    {
        var entity = await context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
        if (entity == null) throw new Exception("Userul nu a fost găsit");

        entity.FirstName = userDto.FirstName;
        entity.LastName = userDto.LastName;
        entity.Email = userDto.Email;
        entity.Money = userDto.Money;

        context.Users.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteUser(int userId)
    {
        var entity = await context.Users.FindAsync(userId);
        if (entity != null)
        {
            context.Users.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}