using EissenhowerMatrixBackend.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Requests.Queries.Todos;

public record GetCompleteTodoItemsQuery : IRequest<List<Todo>>;
