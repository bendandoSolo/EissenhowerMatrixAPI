using EissenhowerMatrixBackend.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Queries;

public record GetTodoItemByIdQuery(int Id) : IRequest<Todo?>;
