using APINet.DataTransferObjects;

namespace APINet.Services.Abstractions;

public interface IUserService
{
    Task<PagedResponse<UserRecord>> GetUsers(SearchPaginationQueryParams queryParams);
    Task<UserRecord?> GetUser(int userId);
    Task AddUser(UserAddRecord user);
    Task UpdateUser(UserUpdateRecord user);
    Task DeleteUser(int userId);
}