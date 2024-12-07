using Microsoft.EntityFrameworkCore;
using ToDoAPI.Entities.Auth;
using ToDoAPI.Entities.Entities;


public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Status> Statuses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Örnek ilişki ayarı: Todo ve User ilişkisi
        modelBuilder.Entity<Todo>()
            .HasOne(t => t.User)
            .WithMany(u => u.Todos)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Todo ve Status ilişkisi
        modelBuilder.Entity<Todo>()
            .HasOne(t => t.Status)
            .WithMany(c => c.Todos)
            .HasForeignKey(t => t.StatusId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


