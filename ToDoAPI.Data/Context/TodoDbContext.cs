using Microsoft.EntityFrameworkCore;
using ToDoAPI.Entities.Auth;
using ToDoAPI.Entities.Entities;


public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Todo>()
       .Property(t => t.Status)
       .HasConversion<string>(); 

    }
}


