using EissenhowerMatrixBackend.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Queries;

public record GetCompleteTodoItemsQuery : IRequest<List<Todo>>;
