
using EissenhowerMatrixBackend.Comands;
using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Handlers;
public class PutTodoItemCommandHandler : IRequestHandler<PutTodoItemCommand, Todo?>
{
    private readonly TodoDb _db;

    public PutTodoItemCommandHandler(TodoDb db)
    {
        _db = db;
    }

    public async Task<Todo?> Handle(PutTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todo = await _db.Todos.FindAsync(request.id);

        if (todo is null) return null;

        todo.Name = request.todo.Name;
        todo.Description = request.todo.Description;
        todo.CompletionDate = request.todo.CompletionDate;
        todo.Priority = request.todo.Priority;
        await _db.SaveChangesAsync();

        return todo;
    }
}
