using EissenhowerMatrixBackend.Models;
using EissenhowerMatrixBackend.Models.ViewModels;
using MediatR;

namespace EissenhowerMatrixBackend.Requests.Commands.Todos;

public record PostTodoItemCommand(CreateTodoViewModel todoViewModel) : IRequest<Todo?>;
