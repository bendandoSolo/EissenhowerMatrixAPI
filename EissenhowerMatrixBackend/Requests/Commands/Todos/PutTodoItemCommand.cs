using EissenhowerMatrixBackend.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Requests.Commands.Todos;

public record PutTodoItemCommand(int id, Todo todo) : IRequest<Todo?>;
