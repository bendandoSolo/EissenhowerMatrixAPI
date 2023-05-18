namespace EissenhowerMatrixBackend.Handlers;
using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.DataBaseConnection.Models;
using EissenhowerMatrixBackend.Queries;
using MediatR;

public class GetTodoItemByIdHandler : IRequestHandler<GetTodoItemByIdQuery, Todo?>
{
    private readonly TodoDb _db;

    public GetTodoItemByIdHandler(TodoDb db)
    {
        _db = db;
    }

    public async Task<Todo?> Handle(GetTodoItemByIdQuery request, CancellationToken cancellationToken)
    {
       return await _db.Todos.FindAsync(request.Id);
    }
}
