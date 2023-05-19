
using EissenhowerMatrixBackend.Comands;
using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Handlers;
using System.Net.Http.Json;

namespace API_Tests.Handlers
{
    public class PutTodoItemCommandHandlerTests
    {

        [Fact]
        public async void CanPutTodo_WithCorrectId()
        {
            // Arrange
            var db = CreateSUT();
            await SeedData(db);
            var handler = new PutTodoItemCommandHandler(db);

            //Act
            var existingTodo = new Todo { Id = 1, Name = "Updated Task", Description = "Updated Description" }; // assuming an item with Id 1 exists
            Todo? todoItem = await handler.Handle(new PutTodoItemCommand(1, existingTodo), CancellationToken.None);

            // Assert
            Assert.Equal("Updated Task", todoItem?.Name);
            Assert.Equal("Updated Description", todoItem?.Description);
        }


        [Fact]
        public async void CannotPutTodo_WithInCorrectId()
        {
            // Arrange
            var db = CreateSUT();
            var handler = new PutTodoItemCommandHandler(db);

            //Act
            var existingTodo = new Todo { Id = 101, Name = "Updated Task", Description = "Updated Description" }; // assuming an item with Id 1 exists
            Todo? todoItem = await handler.Handle(new PutTodoItemCommand(1, existingTodo), CancellationToken.None);

            // Assert
            Assert.Null(todoItem);
        }

        private TodoDb CreateSUT()
        {
            var dbOptions = new DbContextOptionsBuilder<TodoDb>()
                                .UseInMemoryDatabase(databaseName: "InMemoryTest_PutTodo")
                                .Options;

            return new TodoDb(dbOptions);
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
}
