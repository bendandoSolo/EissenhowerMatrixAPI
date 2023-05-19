
using EissenhowerMatrixBackend.Comands;
using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Handlers;

namespace API_Tests.Handlers
{
    public class PostTodoItemCommandHandlerTests
    {
        [Fact]
        public async void CanPostTodo()
        {
            // Arrange
            var db = CreateSUT();
            await SeedData(db);
            var handler = new PostTodoItemCommandHandler(db);

            //Act
            var newTodo = new Todo { Name = "New Task", Description = "New Description" };
            Todo? todoItem = await handler.Handle(new PostTodoItemCommand(newTodo), CancellationToken.None);

            // Assert
            Assert.Equal("New Task", todoItem?.Name);
            Assert.Equal("New Description", todoItem?.Description);
        }

        private TodoDb CreateSUT()
        {
            var dbOptions = new DbContextOptionsBuilder<TodoDb>()
                                .UseInMemoryDatabase(databaseName: "InMemoryTest_PostTodo")
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
