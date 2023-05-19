using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Handlers;
using EissenhowerMatrixBackend.Queries;

namespace API_Tests.Handlers
{
    public class GetTodoItemByIdHandlerTests
    {

        [Fact]
        public async void ReturnsCorrectTodo()
        {
            // Arrange
            var db = CreateSUT();
            await SeedData(db);
            var handler = new GetTodoItemByIdQueryHandler(db);

            // Act
            Todo? todoItem = await handler.Handle(new GetTodoItemByIdQuery(1), CancellationToken.None);

            // Assert
            Assert.Equal(1, todoItem?.Id);
        }

        private TodoDb CreateSUT()
        {
            var dbOptions = new DbContextOptionsBuilder<TodoDb>()
                                .UseInMemoryDatabase(databaseName: "InMemoryTest2")
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
