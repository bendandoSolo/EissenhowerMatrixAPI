using EissenhowerMatrixBackend.DataBaseConnection.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Comands;

public record PostTodoItemCommand(Todo todo) : IRequest<Todo?>;
