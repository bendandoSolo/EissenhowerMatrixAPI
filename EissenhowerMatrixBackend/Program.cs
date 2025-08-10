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
using EissenhowerMatrixBackend.Comands;
using EissenhowerMatrixBackend.Commands;
using Microsoft.Extensions.Configuration;
using Scalar.AspNetCore;

const string CorsPolicyName = "_myCorsPolicy";

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList")); 
builder.Services.AddDbContext<TodoDb>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddCors(options
  => options.AddPolicy(name: CorsPolicyName, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTodoItemsQueryHandler).Assembly));
//builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors(CorsPolicyName);

app.MapGet("/", () => "Hello World!");

app.MapGet("/todoitems", async (IMediator mediator) => await mediator.Send(new GetTodoItemsQuery()));

app.MapGet("/todoitems/{id}", async (int id, IMediator mediator) => await mediator.Send(new GetTodoItemByIdQuery(id)).ToOkOrNotFound());

app.MapGet("/todoitems/complete", async (IMediator mediator) => await mediator.Send(new GetCompleteTodoItemsQuery()));

app.MapPost("/todoitems", async (Todo todo, IMediator mediator) => await mediator.Send(new PostTodoItemCommand(todo)).ToTodoOrNotFound());

app.MapPut("/todoitems/{id}", async (int id, Todo todo, IMediator mediator) => await mediator.Send(new PutTodoItemCommand(id,todo)).ToNoContentOrNotFound());

app.MapDelete("/todoitems/{id}", async (int id, IMediator mediator) => await mediator.Send(new DeleteTodoItemByIdCommand(id)).ToOkOrNotFound());

app.Run();

public partial class Program { }