using APINet.Database.Models;

namespace APINet.DataTransferObjects;

public class PaginationQueryParams
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}