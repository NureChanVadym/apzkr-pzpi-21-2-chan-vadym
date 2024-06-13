// IResourceTypeService.cs
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;

public interface IResourceTypeService
{
    Task<IEnumerable<ResourceTypeViewModel>> GetAllAsync();
    Task<ResourceTypeViewModel> GetByIdAsync(int id);
    Task<ResourceTypeViewModel> CreateAsync(ResourceTypeCreateViewModel resourceTypeCreateViewModel);
    Task<ResourceTypeViewModel> UpdateAsync(int id, ResourceTypeEditViewModel resourceTypeEditViewModel);
    Task DeleteAsync(int id);
}