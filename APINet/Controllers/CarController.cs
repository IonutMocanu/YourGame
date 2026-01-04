using Microsoft.AspNetCore.Mvc;
using APINet.Services.Abstractions;
using APINet.Shared.DataTransferObjects;

namespace APINet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<CarRecord>>> GetCars([FromQuery] SearchPaginationQueryParams query)
    {
        var response = await _carService.GetCarsPaged(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarRecord>> GetCar(int id)
    {
        var car = await _carService.GetCarById(id);
        if (car == null) return NotFound("Mașina nu a fost găsită.");
        
        return Ok(car);
    }

    [HttpPost("buy/{userId}")]
    public async Task<IActionResult> BuyCar(int userId, [FromBody] CarAddRecord car)
    {
        try 
        {
            await _carService.BuyCar(userId, car);
            return Ok(car);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        await _carService.DeleteCar(id);
        return Ok("Mașina a fost ștearsă.");
    }
}