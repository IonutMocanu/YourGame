using APINet.Shared.DataTransferObjects;

namespace APINet.Services.Abstractions;

public interface IUserService
{
    Task<PagedResponse<UserRecord>> GetUsers(SearchPaginationQueryParams queryParams);
    Task<UserRecord?> GetUser(string email);
    Task AddUser(UserAddRecord user);
    Task UpdateUser(UserUpdateRecord user);
    Task DeleteUser(int userId);
    Task AddMoney(UserMoneyUpdateRecord userDto);
}