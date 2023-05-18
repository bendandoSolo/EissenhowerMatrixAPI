using EissenhowerMatrixBackend.DataBaseConnection.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Queries;

public record GetTodoItemByIdQuery(int Id) : IRequest<Todo?>;
