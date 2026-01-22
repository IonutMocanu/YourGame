using Microsoft.EntityFrameworkCore;
using APINet.Database;
using APINet.Shared.Database.Models;
using APINet.Shared.DataTransferObjects;
using APINet.Services.Abstractions;

namespace APINet.Services.Implementations;

public class CarService(GameDatabaseContext context) : ICarService
{
    public async Task<PagedResponse<CarRecord>> GetCarsPaged(SearchPaginationQueryParams queryParams)
    {
        var query = context.Cars.AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var search = queryParams.Search.Trim().ToLower();
            query = query.Where(c => 
                c.Manufacturer.ToLower().Contains(search) || 
                c.Model.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync();

        var data = await query
            .Include(c => c.User) 
            .OrderBy(c => c.Manufacturer)
            .Skip((queryParams.Page - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .Select(c => new CarRecord
            {
                Id = c.Id,
                Manufacturer = c.Manufacturer,
                Model = c.Model,
                Year = c.Year,
                Speed = c.Speed,
                Price = c.Price,
                OwnerName = c.User != null ? $"{c.User.FirstName} {c.User.LastName}" : "Fără Proprietar"
            })
            .ToListAsync();

        return new PagedResponse<CarRecord>
        {
            Page = queryParams.Page,
            PageSize = queryParams.PageSize,
            TotalCount = totalCount,
            Data = data
        };
    }

    public async Task<CarRecord?> GetCarById(int id)
    {
        return await context.Cars
            .Include(c => c.User)
            .Where(c => c.Id == id)
            .Select(c => new CarRecord
            {
                Id = c.Id,
                Manufacturer = c.Manufacturer,
                Model = c.Model,
                Year = c.Year,
                Speed = c.Speed,
                Price = c.Price,
                OwnerName = c.User != null ? $"{c.User.FirstName} {c.User.LastName}" : "Fără Proprietar"
            })
            .FirstOrDefaultAsync();
    }

    public async Task BuyCar(int userId, CarAddRecord carDto)
    {
        var userExists = await context.Users.AnyAsync(u => u.Id == userId);
        if (!userExists) throw new Exception("Userul nu există!");

        var entity = new Car
        {
            UserId = userId, 
            Manufacturer = carDto.Manufacturer,
            Model = carDto.Model,
            Year = carDto.Year,
            Speed = carDto.Speed,
            Price = carDto.Price
        };

        await context.Cars.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCar(CarUpdateRecord carDto)
    {
        var entity = await context.Cars.FirstOrDefaultAsync(c => c.Id == carDto.Id);
        if (entity == null) throw new Exception("Mașina nu există!");

        if (carDto.Manufacturer != null) entity.Manufacturer = carDto.Manufacturer;
        if (carDto.Model != null) entity.Model = carDto.Model;
        if (carDto.Speed != null) entity.Speed = carDto.Speed.Value;
        if (carDto.Price != null) entity.Price = carDto.Price.Value;

        context.Cars.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCar(int id)
    {
        var entity = await context.Cars.FindAsync(id);
        if (entity != null)
        {
            context.Cars.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}