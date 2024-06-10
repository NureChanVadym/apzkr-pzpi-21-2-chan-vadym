// IoTDeviceController.cs

using EcoMeChan.Services.Interfaces;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using EcoMeChan.ViewModels.Extended;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class IoTDeviceController : ControllerBase
{
    private readonly IIoTDeviceService _deviceService;

    public IoTDeviceController(IIoTDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IoTDeviceCreateViewModel deviceCreateViewModel)
    {
        try
        {
            var createdDevice = await _deviceService.CreateAsync(deviceCreateViewModel);
            return CreatedAtAction(nameof(Get), new { id = createdDevice.Id }, createdDevice);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IoTDeviceExtendedViewModel>> Get(int id)
    {
        try
        {
            var device = await _deviceService.GetAsync(id);
            return Ok(device);
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

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<IoTDeviceViewModel>>> GetByUserId(int userId)
    {
        try
        {
            var devices = await _deviceService.GetByUserIdAsync(userId);
            return Ok(devices);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IoTDeviceViewModel>>> GetAll()
    {
        try
        {
            var devices = await _deviceService.GetAllAsync();
            return Ok(devices);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] IoTDeviceEditViewModel deviceEditViewModel)
    {
        try
        {
            await _deviceService.UpdateAsync(id, deviceEditViewModel);
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
            await _deviceService.DeleteAsync(id);
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