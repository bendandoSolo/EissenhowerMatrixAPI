using EissenhowerMatrixBackend.DataBaseConnection.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Commands;
    public record DeleteTodoItemByIdCommand(int id) : IRequest<Todo?>;
