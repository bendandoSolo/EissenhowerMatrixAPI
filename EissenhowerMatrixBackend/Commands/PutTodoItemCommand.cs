using EissenhowerMatrixBackend.DataBaseConnection.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Comands;

public record PutTodoItemCommand(int id, Todo todo) : IRequest<Todo?>;
