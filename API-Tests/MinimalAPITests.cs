﻿
using EissenhowerMatrixBackend.DataBaseConnection.Models;
using System.Net.Http.Json;
namespace API_Tests;

public class TodoControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public TodoControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CanGetAll_TodoItems()
    {
        var client = _factory.CreateClient();
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
        // await SeedData(TodoDb);

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
        var existingTodo = new Todo { Id = 1, Name = "Updated Task", Description = "Updated Description" }; // assuming an item with Id 1 exists

        // Act
        var response = await client.PutAsJsonAsync($"/todoitems/{existingTodo.Id}", existingTodo);

        // Assert
        response.EnsureSuccessStatusCode();

        // we should do some better checks but that would require us to read the item back from the API or get access to the database from this class

        //var todoItem = await response.Content.ReadFromJsonAsync<Todo>();
        //_factory.db.Todos.Find(existingTodo.Id);
        //Todo todoItem = client.
        //Assert.Equal(existingTodo.Name, todoItem?.Name);
        //Assert.Equal(existingTodo.Description, todoItem?.Description);
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
   
}

