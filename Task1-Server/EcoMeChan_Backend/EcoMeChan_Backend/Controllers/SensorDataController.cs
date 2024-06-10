// SensorDataController.cs

using EcoMeChan.Services.Interfaces;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SensorDataController : ControllerBase
{
    private readonly ISensorDataService _sensorDataService;

    public SensorDataController(ISensorDataService sensorDataService)
    {
        _sensorDataService = sensorDataService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SensorDataCreateViewModel sensorDataCreateViewModel)
    {
        try
        {
            var createdSensorData = await _sensorDataService.CreateAsync(sensorDataCreateViewModel);
            return CreatedAtAction(nameof(Get), new { id = createdSensorData.Id }, createdSensorData);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SensorDataViewModel>> Get(int id)
    {
        try
        {
            var sensorData = await _sensorDataService.GetAsync(id);
            return Ok(sensorData);
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
    public async Task<ActionResult<IEnumerable<SensorDataViewModel>>> GetAll()
    {
        try
        {
            var sensorDataList = await _sensorDataService.GetAllAsync();
            return Ok(sensorDataList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("device/{deviceId}")]
    public async Task<ActionResult<IEnumerable<SensorDataViewModel>>> GetByDeviceId(int deviceId)
    {
        try
        {
            var sensorDataList = await _sensorDataService.GetByDeviceIdAsync(deviceId);
            return Ok(sensorDataList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SensorDataEditViewModel sensorDataEditViewModel)
    {
        try
        {
            await _sensorDataService.UpdateAsync(id, sensorDataEditViewModel);
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
            await _sensorDataService.DeleteAsync(id);
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