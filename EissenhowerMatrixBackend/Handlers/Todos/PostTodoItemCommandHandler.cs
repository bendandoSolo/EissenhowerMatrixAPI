using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using EissenhowerMatrixBackend.Models.ViewModels;
using EissenhowerMatrixBackend.Requests.Queries;
using EissenhowerMatrixBackend.Requests.Commands.Todos;
using MediatR;

namespace EissenhowerMatrixBackend.Handlers.Todos;
public class PostTodoItemCommandHandler : IRequestHandler<PostTodoItemCommand, Todo?>
{
    private readonly TodoDb _db;

    public PostTodoItemCommandHandler(TodoDb db)
    {
        _db = db;
    }

    public async Task<Todo?> Handle(PostTodoItemCommand request, CancellationToken cancellationToken)
    {
        CreateTodoViewModel createTodoView = request.todoViewModel;

        Todo newTodo = new Todo
        {
            Name = createTodoView.Name,
            Description = createTodoView.Description,
            Priority = createTodoView.Priority,
            ToBuyOrGet = createTodoView.ToBuyOrGet,
            ProjectId = createTodoView.ProjectId,
        };

        _db.Todos.Add(newTodo);
        
        await _db.SaveChangesAsync();

        return newTodo;
    }
}
