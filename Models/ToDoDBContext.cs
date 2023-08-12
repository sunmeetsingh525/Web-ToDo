using Microsoft.EntityFrameworkCore;

namespace ToDoListMvc.Models
{
// Represents the database context for ToDoItem entities
    public class ToDoDbContext : DbContext
    {
        // Constructor initializes the context with provided options
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
            : base(options)
        {
        }
        // Collection of ToDoItem entities in the database.
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}