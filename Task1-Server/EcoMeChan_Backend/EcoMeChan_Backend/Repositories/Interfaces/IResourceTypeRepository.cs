// IResourceTypeRepository.cs
using EcoMeChan.Models;

public interface IResourceTypeRepository
{
    Task<IEnumerable<ResourceType>> GetAllAsync();
    Task<ResourceType> GetByIdAsync(int id);
    Task<ResourceType> CreateAsync(ResourceType resourceType);
    Task<ResourceType> UpdateAsync(ResourceType resourceType);
    Task DeleteAsync(int id);
}