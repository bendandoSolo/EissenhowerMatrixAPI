namespace EissenhowerMatrixBackend.Handlers.Todos;
using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using EissenhowerMatrixBackend.Requests.Queries.Todos;
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
