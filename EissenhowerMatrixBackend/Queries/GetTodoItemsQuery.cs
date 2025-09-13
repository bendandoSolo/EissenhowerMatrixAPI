using EissenhowerMatrixBackend.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Queries;

public record GetTodoItemsQuery : IRequest<List<Todo>>;
