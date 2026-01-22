using Microsoft.EntityFrameworkCore;
using APINet.Database;
using APINet.Shared.Database.Models;
using APINet.Shared.DataTransferObjects;
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


    var totalCount = await query.CountAsync();


    var userEntities = await query
        .Include(u => u.Cars) 
        .OrderBy(u => u.Id)
        .Skip((queryParams.Page - 1) * queryParams.PageSize)
        .Take(queryParams.PageSize)
        .ToListAsync(); 


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

    public async Task<UserRecord?> GetUser(string email)
{
    var userEntity = await context.Users
        .Include(u => u.Cars) 
        .FirstOrDefaultAsync(u => u.Email == email);

    if (userEntity == null) 
    {
        return null;
    }


    var userDto = new UserRecord
    {
        Id = userEntity.Id,
        FirstName = userEntity.FirstName,
        LastName = userEntity.LastName,
        Email = userEntity.Email,
        Money = userEntity.Money,
        
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
            Email = userDto.Email,
            Money = 100
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

    public async Task AddMoney(UserMoneyUpdateRecord userDto)
    {
        var entity = await context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
        if (entity == null) throw new Exception("Userul nu a fost găsit");
        entity.Money = userDto.Money;
        context.Users.Update(entity);
        await context.SaveChangesAsync();
    }
}