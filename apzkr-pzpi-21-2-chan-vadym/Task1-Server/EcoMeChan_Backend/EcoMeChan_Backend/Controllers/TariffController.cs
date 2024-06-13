// TariffController.cs

using EcoMeChan.Enums;
using EcoMeChan.Services.Interfaces;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TariffController : ControllerBase
{
    private readonly ITariffService _tariffService;

    public TariffController(ITariffService tariffService)
    {
        _tariffService = tariffService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TariffCreateViewModel tariffCreateViewModel)
    {
        try
        {
            var createdTariff = await _tariffService.CreateAsync(tariffCreateViewModel);
            return CreatedAtAction(nameof(Get), new { id = createdTariff.Id }, createdTariff);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TariffViewModel>> Get(int id)
    {
        try
        {
            var tariff = await _tariffService.GetAsync(id);
            return Ok(tariff);
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TariffViewModel>>> GetAll()
    {
        try
        {
            var tariffs = await _tariffService.GetAllAsync();
            return Ok(tariffs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("resourceType/{resourceTypeId}")]
    public async Task<ActionResult<IEnumerable<TariffViewModel>>> GetByResourceTypeId(int resourceTypeId)
    {
        try
        {
            var tariffs = await _tariffService.GetByResourceTypeIdAsync(resourceTypeId);
            return Ok(tariffs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TariffEditViewModel tariffEditViewModel)
    {
        try
        {
            await _tariffService.UpdateAsync(id, tariffEditViewModel);
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
            await _tariffService.DeleteAsync(id);
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