using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.DataBaseConnection.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MediatR;
using EissenhowerMatrixBackend.Queries;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using EissenhowerMatrixBackend.Handlers;
using EissenhowerMatrixBackend.Extensions;

const string CorsPolicyName = "_myCorsPolicy";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddCors(options
  => options.AddPolicy(name: CorsPolicyName, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTodoItemsHandler).Assembly));

var app = builder.Build();

app.UseCors(CorsPolicyName);

app.MapGet("/", () => "Hello World!");

app.MapGet("/todoitems", async (IMediator mediator) => await mediator.Send(new GetTodoItemsQuery()));

//app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
//   await db.Todos.FindAsync(id)
//       is Todo todo
//           ? Results.Ok(todo)
//           : Results.NotFound());

app.MapGet("/todoitems/{id}", async (int id, IMediator mediator) => await mediator.Send(new GetTodoItemByIdQuery(id)).ToOkOrNotFound());

app.MapGet("/todoitems/complete", async (TodoDb db) =>
    await db.Todos.Where(t => t.CompletionDate != null ).ToListAsync());

app.MapPost("/todoitems", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.Description = inputTodo.Description;
    todo.CompletionDate = inputTodo.CompletionDate;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(todo);
    }

    return Results.NotFound();
});

app.Run();

public partial class Program { }