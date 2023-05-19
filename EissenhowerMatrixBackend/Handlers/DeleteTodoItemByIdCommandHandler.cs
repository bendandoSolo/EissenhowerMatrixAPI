
namespace EissenhowerMatrixBackend.Handlers;

using EissenhowerMatrixBackend.Commands;
using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.DataBaseConnection.Models;
using EissenhowerMatrixBackend.Queries;
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
