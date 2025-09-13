using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using EissenhowerMatrixBackend.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EissenhowerMatrixBackend.Handlers;

public class GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, List<Todo>>
{
    private readonly TodoDb _db;

    public GetTodoItemsQueryHandler(TodoDb db)
    {
        _db = db;
    }

    public async Task<List<Todo>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        return await _db.Todos.ToListAsync(cancellationToken);
    }
}
