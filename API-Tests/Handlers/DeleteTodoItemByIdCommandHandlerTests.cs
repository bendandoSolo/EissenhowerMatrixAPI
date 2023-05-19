using EissenhowerMatrixBackend.Commands;
using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Handlers;

namespace API_Tests.Handlers
{
    public class DeleteTodoItemByIdCommandHandlerTests
    {
        [Fact]
        public async void CanDeleteTodo()
        {
            // Arrange
            var db = CreateSUT();
            await SeedData(db);
            var handler = new DeleteTodoItemByIdCommandHandler(db);

            //Act
            var id = 1;
            Todo? todoItem = await handler.Handle(new DeleteTodoItemByIdCommand(id), CancellationToken.None);

            // Assert
            Assert.Equal(id, todoItem?.Id);
        }

        private TodoDb CreateSUT()
        {
            var dbOptions = new DbContextOptionsBuilder<TodoDb>()
                                .UseInMemoryDatabase(databaseName: "InMemoryTest_DeleteTodo")
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
