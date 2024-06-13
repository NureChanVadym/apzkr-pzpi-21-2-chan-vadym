// ResourceTypeRepository.cs
using EcoMeChan.Database;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ResourceTypeRepository : IResourceTypeRepository
{
    private readonly ApplicationDbContext _context;

    public ResourceTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ResourceType>> GetAllAsync()
    {
        return await _context.ResourceTypes.ToListAsync();
    }

    public async Task<ResourceType> GetByIdAsync(int id)
    {
        return await _context.ResourceTypes.FindAsync(id);
    }

    public async Task<ResourceType> CreateAsync(ResourceType resourceType)
    {
        _context.ResourceTypes.Add(resourceType);
        await _context.SaveChangesAsync();
        return resourceType;
    }

    public async Task<ResourceType> UpdateAsync(ResourceType resourceType)
    {
        _context.ResourceTypes.Update(resourceType);
        await _context.SaveChangesAsync();
        return resourceType;
    }

    public async Task DeleteAsync(int id)
    {
        var resourceType = await _context.ResourceTypes.FindAsync(id);
        if (resourceType != null)
        {
            _context.ResourceTypes.Remove(resourceType);
            await _context.SaveChangesAsync();
        }
    }
}