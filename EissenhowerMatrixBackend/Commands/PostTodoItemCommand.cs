using EissenhowerMatrixBackend.Models;
using EissenhowerMatrixBackend.Models.ViewModels;
using MediatR;

namespace EissenhowerMatrixBackend.Comands;

public record PostTodoItemCommand(CreateTodoViewModel todoViewModel) : IRequest<Todo?>;
