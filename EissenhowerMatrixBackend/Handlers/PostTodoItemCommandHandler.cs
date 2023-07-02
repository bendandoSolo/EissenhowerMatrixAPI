
using EissenhowerMatrixBackend.Comands;
using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.DataBaseConnection.Models;
using EissenhowerMatrixBackend.Queries;
using MediatR;

namespace EissenhowerMatrixBackend.Handlers;
public class PostTodoItemCommandHandler : IRequestHandler<PostTodoItemCommand, Todo?>
{
    private readonly TodoDb _db;

    public PostTodoItemCommandHandler(TodoDb db)
    {
        _db = db;
    }

    public async Task<Todo?> Handle(PostTodoItemCommand request, CancellationToken cancellationToken)
    {
        Todo todo = request.todo;
        _db.Todos.Add(todo);
        
        await _db.SaveChangesAsync();

        return todo;
    }
}
