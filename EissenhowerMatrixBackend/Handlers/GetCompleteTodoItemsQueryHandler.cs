using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using EissenhowerMatrixBackend.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EissenhowerMatrixBackend.Handlers;

public class GetCompleteTodoItemsQueryHandler : IRequestHandler<GetCompleteTodoItemsQuery, List<Todo>>
{
    private readonly TodoDb _db;

    public GetCompleteTodoItemsQueryHandler(TodoDb db)
    {
        _db = db;
    }

    public async Task<List<Todo>> Handle(GetCompleteTodoItemsQuery request, CancellationToken cancellationToken)
    {
        return await _db.Todos.Where(todo => todo.CompletionDate != null).ToListAsync(cancellationToken);
    }
}
