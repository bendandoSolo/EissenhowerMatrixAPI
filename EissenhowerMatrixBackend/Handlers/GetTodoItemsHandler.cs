using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.DataBaseConnection.Models;
using EissenhowerMatrixBackend.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EissenhowerMatrixBackend.Handlers;

public class GetTodoItemsHandler : IRequestHandler<GetTodoItemsQuery, List<Todo>>
{
    private readonly TodoDb _db;

    public GetTodoItemsHandler(TodoDb db)
    {
        _db = db;
    }

    public async Task<List<Todo>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        return await _db.Todos.ToListAsync(cancellationToken);
    }
}
