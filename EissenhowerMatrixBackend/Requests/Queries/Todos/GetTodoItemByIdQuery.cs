using EissenhowerMatrixBackend.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Requests.Queries.Todos;

public record GetTodoItemByIdQuery(int Id) : IRequest<Todo?>;
