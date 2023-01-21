using Microsoft.EntityFrameworkCore;
using TodoListsApp.Models;

namespace TodoListsApp.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<TodoList> TodoLists { get; set; }
    }
}
