namespace EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.DataBaseConnection.Models;
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
    }

    public DbSet<Todo> Todos => Set<Todo>();
}

