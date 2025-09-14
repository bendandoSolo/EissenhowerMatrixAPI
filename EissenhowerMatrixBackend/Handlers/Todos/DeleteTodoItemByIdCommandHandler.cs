namespace EissenhowerMatrixBackend.Handlers.Todos;

using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using EissenhowerMatrixBackend.Requests.Queries;
using EissenhowerMatrixBackend.Requests.Commands.Todos;
using MediatR;

public class DeleteTodoItemByIdCommandHandler : IRequestHandler<DeleteTodoItemByIdCommand, Todo?>
{
    private readonly TodoDb _db;

    public DeleteTodoItemByIdCommandHandler(TodoDb db)
    {
        _db = db;
    }

    public async Task<Todo?> Handle(DeleteTodoItemByIdCommand request, CancellationToken cancellationToken)
    {
        if (await _db.Todos.FindAsync(request.id) is Todo todo)
        {
            _db.Todos.Remove(todo);
            await _db.SaveChangesAsync();
            return todo;
        }

        return null;
    }
}
