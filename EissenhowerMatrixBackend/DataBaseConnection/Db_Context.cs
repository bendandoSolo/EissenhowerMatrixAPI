namespace EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using EissenhowerMatrixBackend.Constants.Enums;
public class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .Property(e => e.Priority)
            .HasConversion(new EnumToStringConverter<EissenhowerStatus>());

        // Global query filter for soft-deleted projects
        modelBuilder.Entity<Project>()
            .HasQueryFilter(p => p.Deleted == null);
    }

    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<Project> Projects => Set<Project>();
}

