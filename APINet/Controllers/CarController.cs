using Microsoft.AspNetCore.Mvc;
using APINet.Services.Abstractions;
using APINet.DataTransferObjects;

namespace APINet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    // "Injectăm" serviciul prin constructor
    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    // GET: api/Car?Page=1&PageSize=10&Search=BMW
    [HttpGet]
    public async Task<ActionResult<PagedResponse<CarRecord>>> GetCars([FromQuery] SearchPaginationQueryParams query)
    {
        var response = await _carService.GetCarsPaged(query);
        return Ok(response);
    }

    // GET: api/Car/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CarRecord>> GetCar(int id)
    {
        var car = await _carService.GetCarById(id);
        if (car == null) return NotFound("Mașina nu a fost găsită.");
        
        return Ok(car);
    }

    // POST: api/Car/buy/5 (Cumpără mașină pentru userul 5)
    [HttpPost("buy/{userId}")]
    public async Task<IActionResult> BuyCar(int userId, [FromBody] CarAddRecord car)
    {
        try 
        {
            await _carService.BuyCar(userId, car);
            return Ok("Ai cumpărat mașina cu succes!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Car (Update tuning/vopsea)
    [HttpPut]
    public async Task<IActionResult> UpdateCar([FromBody] CarUpdateRecord car)
    {
        try
        {
            await _carService.UpdateCar(car);
            return Ok("Mașina a fost actualizată.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Car/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        await _carService.DeleteCar(id);
        return Ok("Mașina a fost ștearsă.");
    }
}