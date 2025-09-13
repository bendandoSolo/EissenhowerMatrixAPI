
using EissenhowerMatrixBackend.Comands;
using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using EissenhowerMatrixBackend.Models.ViewModels;
using EissenhowerMatrixBackend.Queries;
using MediatR;

namespace EissenhowerMatrixBackend.Handlers;
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
        };

        _db.Todos.Add(newTodo);
        
        await _db.SaveChangesAsync();

        return newTodo;
    }
}
