
using EissenhowerMatrixBackend.DataBaseConnection.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using EissenhowerMatrixBackend.DataBaseConnection;
namespace API_Tests;
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
        //await SeedData(TodoDb);
        var response = await client.GetAsync("/todoitems");

        response.EnsureSuccessStatusCode();

        var todoItems = await response.Content.ReadFromJsonAsync<Todo[]>();

       Assert.NotNull(todoItems);
       Assert.Equal(2, todoItems?.Length);
    }
    [Fact]
    public async Task GetAPI_CanGetAllTodos()
    {
        // Arrange
        var client = _factory.CreateClient();
         await SeedData(TodoDb);

        // Act
        var response = await client.GetAsync("/todoitems/complete");

        // Assert
        response.EnsureSuccessStatusCode();
        var todoItems = await response.Content.ReadFromJsonAsync<Todo[]>();
        Assert.Contains(todoItems, item => item.CompletionDate != null);
        // Assert.All(todoItems, item => Assert.True(item.CompletionDate != null));
    }

    [Fact]
    public async Task GetAPI_CanGetItemById()
    {
        // Arrange
        var client = _factory.CreateClient();
       // await SeedData(TodoDb);
        var id = 1; // assuming you have an item with Id 1

        // Act
        var response = await client.GetAsync($"/todoitems/{id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var todoItem = await response.Content.ReadFromJsonAsync<Todo>();
        Assert.Equal(id, todoItem?.Id);
    }

    [Fact]
    public async Task TestPostTodoItem()
    {
        // Arrange
        var client = _factory.CreateClient();
        var newTodo = new Todo { Name = "New Task", Description = "New Description" };

        // Act
        var response = await client.PostAsJsonAsync("/todoitems", newTodo);

        // Assert
        response.EnsureSuccessStatusCode();
        var todoItem = await response.Content.ReadFromJsonAsync<Todo>();
        Assert.Equal(newTodo.Name, todoItem?.Name);
        Assert.Equal(newTodo.Description, todoItem?.Description);
    }

    [Fact]
    public async Task TestPutTodoItem()
    {
        // Arrange
        var client = _factory.CreateClient();
       // await SeedData(TodoDb);
        var existingTodo = new Todo { Id = 3, Name = "Updated Task", Description = "Updated Description" }; // assuming an item with Id 1 exists

        // Act
        var response = await client.PutAsJsonAsync($"/todoitems/{existingTodo.Id}", existingTodo);

        // Assert
        response.EnsureSuccessStatusCode();
        var todoItem = await response.Content.ReadFromJsonAsync<Todo>();
        Assert.Equal(existingTodo.Name, todoItem?.Name);
        Assert.Equal(existingTodo.Description, todoItem?.Description);
    }

    [Fact]
    public async Task TestDeleteTodoItem()
    {
        // Arrange
        var client = _factory.CreateClient();
        // await SeedData(TodoDb);
        var id = 1; // assuming you have an item with Id 1

        // Act
        var response = await client.DeleteAsync($"/todoitems/{id}");

        // Assert
        response.EnsureSuccessStatusCode();

        // Verify the item is deleted
        var getResponse = await client.GetAsync($"/todoitems/{id}");
        Assert.False(getResponse.IsSuccessStatusCode);
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

