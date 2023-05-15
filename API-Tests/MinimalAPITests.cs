using EissenhowerMatrixBackend.DataBaseConnection.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using EissenhowerMatrixBackend.DataBaseConnection;

public class TodoControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private static TodoDb TodoDb { get; set; }

    public TodoControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<TodoDb>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTestDatabase");
                });
            });
        });

        // Get a service scope to get the TodoDb instance
        var scope = _factory.Services.CreateScope();
        TodoDb = scope.ServiceProvider.GetRequiredService<TodoDb>();
    }

    [Fact]
    public async Task TestGetTodoItems()
    {
        var client = _factory.CreateClient();
        await SeedData(TodoDb);
        var response = await client.GetAsync("/todoitems");

        response.EnsureSuccessStatusCode();

        var todoItems = await response.Content.ReadFromJsonAsync<Todo[]>();

       Assert.NotNull(todoItems);
       Assert.Equal(2, todoItems?.Length);
    }

    [Fact]
    public async Task TestGetTodoItemsComplete()
    {
        // ... Your test logic here
    }

    [Fact]
    public async Task TestGetTodoItemById()
    {
        // ... Your test logic here
    }

    [Fact]
    public async Task TestPostTodoItem()
    {
        // ... Your test logic here
    }

    [Fact]
    public async Task TestPutTodoItem()
    {
        // ... Your test logic here
    }

    [Fact]
    public async Task TestDeleteTodoItem()
    {
        // ... Your test logic here
    }

    private async Task SeedData(TodoDb db)
    {
        // Seed your test data here
        db.Todos.AddRange(
            new Todo { Id = 1, Name = "Task 1", Description = "Description 1" },
            new Todo { Id = 2, Name = "Task 2", Description = "Description 2", CompletionDate = DateTime.Now }
        );

        await db.SaveChangesAsync();
    }
}

