// ResourceTypeService.cs
using AutoMapper;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using EcoMeChan.Services.Interfaces;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;

public class ResourceTypeService : IResourceTypeService
{
    private readonly IResourceTypeRepository _resourceTypeRepository;
    private readonly IMapper _mapper;

    public ResourceTypeService(IResourceTypeRepository resourceTypeRepository, IMapper mapper)
    {
        _resourceTypeRepository = resourceTypeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ResourceTypeViewModel>> GetAllAsync()
    {
        var resourceTypes = await _resourceTypeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ResourceTypeViewModel>>(resourceTypes);
    }

    public async Task<ResourceTypeViewModel> GetByIdAsync(int id)
    {
        var resourceType = await _resourceTypeRepository.GetByIdAsync(id);
        return _mapper.Map<ResourceTypeViewModel>(resourceType);
    }

    public async Task<ResourceTypeViewModel> CreateAsync(ResourceTypeCreateViewModel resourceTypeCreateViewModel)
    {
        var resourceType = _mapper.Map<ResourceType>(resourceTypeCreateViewModel);
        var createdResourceType = await _resourceTypeRepository.CreateAsync(resourceType);
        return _mapper.Map<ResourceTypeViewModel>(createdResourceType);
    }

    public async Task<ResourceTypeViewModel> UpdateAsync(int id, ResourceTypeEditViewModel resourceTypeEditViewModel)
    {
        var existingResourceType = await _resourceTypeRepository.GetByIdAsync(id);
        if (existingResourceType == null)
        {
            throw new InvalidOperationException("Resource type not found.");
        }

        _mapper.Map(resourceTypeEditViewModel, existingResourceType);
        var updatedResourceType = await _resourceTypeRepository.UpdateAsync(existingResourceType);
        return _mapper.Map<ResourceTypeViewModel>(updatedResourceType);
    }

    public async Task DeleteAsync(int id)
    {
        await _resourceTypeRepository.DeleteAsync(id);
    }
}