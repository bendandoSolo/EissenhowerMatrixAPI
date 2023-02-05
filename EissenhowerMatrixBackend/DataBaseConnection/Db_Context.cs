using EissenhowerMatrixBackend.DataBaseConnection.Models;
using Microsoft.EntityFrameworkCore;

namespace EissenhowerMatrixBackend.DataBaseConnection
{
        class TodoDb : DbContext
        {
            public TodoDb(DbContextOptions<TodoDb> options)
                : base(options) { }

            public DbSet<Todo> Todos => Set<Todo>();
        }

}
