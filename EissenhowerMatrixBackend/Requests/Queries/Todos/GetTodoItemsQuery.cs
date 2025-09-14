using EissenhowerMatrixBackend.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Requests.Queries.Todos;

public record GetTodoItemsQuery : IRequest<List<Todo>>;
