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

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var search = queryParams.Search.Trim().ToLower();
            query = query.Where(u => u.Email.ToLower().Contains(search) || u.LastName.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync();

        var data = await query
            .Include(u => u.Cars) // <--- IMPORTANT: Include mașinile pentru a le număra sau afișa
            .OrderBy(u => u.Id)
            .Skip((queryParams.Page - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .Select(u => new UserRecord
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                // Aici populăm garajul userului în lista de useri
                Garage = u.Cars.Select(c => new CarRecord 
                {
                    Id = c.Id,
                    Manufacturer = c.Manufacturer,
                    Model = c.Model,
                    Year = c.Year,
                    Speed = c.Speed,
                    Price = c.Price,
                    OwnerName = $"{u.FirstName} {u.LastName}"
                }).ToList()
            })
            .ToListAsync();

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
        return await context.Users
            .Include(u => u.Cars) // <--- IMPORTANT: Aduce mașinile din baza de date
            .Where(u => u.Id == userId)
            .Select(u => new UserRecord
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                // Transformăm lista de Car (Entities) în CarRecord (DTOs)
                Garage = u.Cars.Select(c => new CarRecord
                {
                    Id = c.Id,
                    Manufacturer = c.Manufacturer,
                    Model = c.Model,
                    Year = c.Year,
                    Speed = c.Speed,
                    Price = c.Price,
                    OwnerName = $"{u.FirstName} {u.LastName}"
                }).ToList()
            })
            .FirstOrDefaultAsync();
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