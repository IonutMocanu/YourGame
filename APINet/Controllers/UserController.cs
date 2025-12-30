using Microsoft.AspNetCore.Mvc;
using APINet.Services.Abstractions;
using APINet.Shared.DataTransferObjects;

namespace APINet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/User?Page=1&PageSize=10
    [HttpGet]
    public async Task<ActionResult<PagedResponse<UserRecord>>> GetUsers([FromQuery] SearchPaginationQueryParams query)
    {
        var response = await _userService.GetUsers(query);
        return Ok(response);
    }

    // GET: api/User/ionut@mocanu
    [HttpGet("{email}")]
    public async Task<ActionResult<UserRecord>> GetUser(string email)
    {
        var user = await _userService.GetUser(email);
        if (user == null) return NotFound("Jucătorul nu există.");

        return Ok(user);
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserAddRecord user)
    {
        await _userService.AddUser(user);
        return Ok(user);
    }

    // PUT: api/User
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRecord user)
    {
        try
        {
            await _userService.UpdateUser(user);
            return Ok("Jucător actualizat.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("add-money")]
    public async Task<IActionResult> AddMoney([FromBody] UserMoneyUpdateRecord user)
    {
        try
        {
            await _userService.AddMoney(user);
            return Ok("Jucător actualizat.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);
        return Ok("Jucător șters.");
    }
}