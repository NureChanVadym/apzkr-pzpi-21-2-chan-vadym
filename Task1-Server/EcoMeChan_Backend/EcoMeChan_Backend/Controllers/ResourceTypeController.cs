// Controllers/ResourceTypeController.cs
using Microsoft.AspNetCore.Mvc;
using EcoMeChan.Services.Interfaces;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;

[ApiController]
[Route("api/[controller]")]
public class ResourceTypeController : ControllerBase
{
    private readonly IResourceTypeService _resourceTypeService;

    public ResourceTypeController(IResourceTypeService resourceTypeService)
    {
        _resourceTypeService = resourceTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResourceTypeViewModel>>> GetAll()
    {
        var resourceTypes = await _resourceTypeService.GetAllAsync();
        return Ok(resourceTypes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResourceTypeViewModel>> GetById(int id)
    {
        var resourceType = await _resourceTypeService.GetByIdAsync(id);
        if (resourceType == null)
        {
            return NotFound();
        }
        return Ok(resourceType);
    }

    [HttpPost]
    public async Task<ActionResult<ResourceTypeViewModel>> Create([FromBody] ResourceTypeCreateViewModel resourceTypeCreateViewModel)
    {
        var createdResourceType = await _resourceTypeService.CreateAsync(resourceTypeCreateViewModel);
        return CreatedAtAction(nameof(GetById), new { id = createdResourceType.Id }, createdResourceType);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResourceTypeViewModel>> Update(int id, [FromBody] ResourceTypeEditViewModel resourceTypeEditViewModel)
    {
        var updatedResourceType = await _resourceTypeService.UpdateAsync(id, resourceTypeEditViewModel);
        return Ok(updatedResourceType);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _resourceTypeService.DeleteAsync(id);
        return NoContent();
    }
}