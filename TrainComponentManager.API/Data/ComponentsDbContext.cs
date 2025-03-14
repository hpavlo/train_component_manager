using Microsoft.EntityFrameworkCore;
using TrainComponentManager.API.Models;

namespace TrainComponentManager.API.Data
{
    public class ComponentsDbContext(DbContextOptions<ComponentsDbContext> options) : DbContext(options)
    {
        public DbSet<Component> Components { get; set; }
    }
}
