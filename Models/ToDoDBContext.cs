using Microsoft.EntityFrameworkCore;

namespace WebToDo.Models
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>().HasData(
                new ToDoItem { Id = 1, Title = "Task 1", Description = "Description 1", IsDone = false },
                new ToDoItem { Id = 2, Title = "Task 2", Description = "Description 2", IsDone = true }
            );
        }
    }
}
