using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Handlers;
using EissenhowerMatrixBackend.Queries;

namespace API_Tests.Handlers
{
    public class GetTodoItemsHandlerTests
    {

        [Fact]
        public async void ReturnsEmptyTodoItems_WhenDatabaseIsEmpty()
        {
            // Arrange
            var db = CreateSUT();
            var handler = new GetTodoItemsHandler(db);

            // Act
            var todoItems = await handler.Handle(new GetTodoItemsQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(0, todoItems?.Count);
        }

        [Fact]
        public async void ReturnsCorrectNumberOfTodoItems_WhenDatabaseIsSeeded()
        {
            // Arrange
            var db = CreateSUT();
            await SeedData(db);
            var handler = new GetTodoItemsHandler(db);

            // Act
            var todoItems = await handler.Handle(new GetTodoItemsQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(2, todoItems?.Count);
        }


        private TodoDb CreateSUT()
        {
            var dbOptions = new DbContextOptionsBuilder<TodoDb>()
                                .UseInMemoryDatabase(databaseName: "InMemoryTest")
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
