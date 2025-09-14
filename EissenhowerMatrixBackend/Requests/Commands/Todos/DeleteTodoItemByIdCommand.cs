using EissenhowerMatrixBackend.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Requests.Commands.Todos;
    public record DeleteTodoItemByIdCommand(int id) : IRequest<Todo?>;
