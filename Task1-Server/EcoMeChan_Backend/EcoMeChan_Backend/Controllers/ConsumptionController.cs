// ConsumptionController.cs

using EcoMeChan.Services.Interfaces;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using EcoMeChan_Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ConsumptionController : ControllerBase
{
    private readonly IConsumptionService _consumptionService;

    public ConsumptionController(IConsumptionService consumptionService)
    {
        _consumptionService = consumptionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ConsumptionCreateViewModel consumptionCreateViewModel)
    {
        try
        {
            var createdConsumption = await _consumptionService.CreateAsync(consumptionCreateViewModel);
            return CreatedAtAction(nameof(Get), new { id = createdConsumption.Id }, createdConsumption);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<ConsumptionViewModel>>> GetByUserId(int userId)
    {
        try
        {
            var consumptions = await _consumptionService.GetByUserIdAsync(userId);
            return Ok(consumptions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConsumptionViewModel>> Get(int id)
    {
        try
        {
            var consumption = await _consumptionService.GetAsync(id);
            return Ok(consumption);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ConsumptionEditViewModel consumptionEditViewModel)
    {
        try
        {
            await _consumptionService.UpdateAsync(id, consumptionEditViewModel);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _consumptionService.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}