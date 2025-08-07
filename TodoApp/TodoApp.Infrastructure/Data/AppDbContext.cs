using Microsoft.EntityFrameworkCore;
using TodoApp.Core.DataModel;

namespace TodoApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoItemModel> TodoItems => Set<TodoItemModel>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
