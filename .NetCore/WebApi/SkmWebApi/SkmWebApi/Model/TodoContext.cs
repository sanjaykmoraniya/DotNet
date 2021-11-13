using Microsoft.EntityFrameworkCore;

namespace SkmWebApi.Model
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            :base(options)
        {
            Database.Migrate();
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Student> Student { get; set; }

    }
}
