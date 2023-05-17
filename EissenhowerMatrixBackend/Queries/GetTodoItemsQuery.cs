using EissenhowerMatrixBackend.DataBaseConnection.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Queries;

public record GetTodoItemsQuery : IRequest<List<Todo>>;
