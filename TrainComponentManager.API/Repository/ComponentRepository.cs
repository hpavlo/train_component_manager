using Microsoft.EntityFrameworkCore;
using TrainComponentManager.API.Data;
using TrainComponentManager.API.Interfaces;
using TrainComponentManager.API.Models;

namespace TrainComponentManager.API.Repository
{
    public class ComponentRepository(ComponentsDbContext context) : IComponentRepository
    {
        private readonly ComponentsDbContext _context = context;

        public async Task CreateAsync(Component component)
        {
            _context.Components.Add(component);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Component>, int)> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Components.CountAsync();
            var components = await _context.Components
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (components, totalCount);
        }

        public async Task<Component?> GetAsync(int id)
        {
            return await _context.Components.FindAsync(id);
        }

        public async Task<Component?> UpdateAsync(Component component)
        {
            var existingComponent = await _context.Components.FindAsync(component.Id);
            if (existingComponent == null)
            {
                return null;
            }

            _context.Entry(existingComponent).CurrentValues.SetValues(component);

            try
            {
                await _context.SaveChangesAsync();
                return component;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null)
            {
                return false;
            }

            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(IEnumerable<Component>, int)> SearchAsync(string searchTerm, int pageNumber, int pageSize)
        {
            IQueryable<Component> query = _context.Components;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query
                    .AsNoTracking()
                    .Where(c => c.Name.Contains(searchTerm) || c.UniqueNumber.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();
            var components = await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (components, totalCount);
        }
    }
}
