using APINet.DataTransferObjects;

namespace APINet.Services.Abstractions;

public interface ICarService
{
    Task<PagedResponse<CarRecord>> GetCarsPaged(SearchPaginationQueryParams queryParams);
    Task<CarRecord?> GetCarById(int id);
    Task BuyCar(int userId, CarAddRecord car);
    Task UpdateCar(CarUpdateRecord car);
    Task DeleteCar(int id);
}