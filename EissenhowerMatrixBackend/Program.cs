using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using Microsoft.EntityFrameworkCore;
using MediatR;
using EissenhowerMatrixBackend.Requests.Queries.Todos;
using EissenhowerMatrixBackend.Extensions;
using EissenhowerMatrixBackend.Requests.Commands.Todos;
using Scalar.AspNetCore;
using EissenhowerMatrixBackend.Requests.Queries.Projects;
using EissenhowerMatrixBackend.Models.ViewModels;
using EissenhowerMatrixBackend.Handlers.Todos;

const string CorsPolicyName = "_myCorsPolicy";

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList")); 
builder.Services.AddDbContext<TodoDb>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddCors(options
  => options.AddPolicy(name: CorsPolicyName, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTodoItemsQueryHandler).Assembly));
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors(CorsPolicyName);

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference(options => //https://localhost:7108/scalar/v1
{
    options
        .WithTitle("Eissenhower Matrix API")
        .WithTheme(ScalarTheme.BluePlanet)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.Axios);
    }
);

app.UseHttpsRedirection();

app.MapGet("/", (IWebHostEnvironment env) =>
{
    if (env.IsDevelopment())
    {
        return Results.Redirect("/scalar/v1");
    }
    return Results.Ok("Eissenhower Matrix API - Production");
});


//Todos API Endpoints
app.MapGet("/todoitems", async (IMediator mediator) => await mediator.Send(new GetTodoItemsQuery()));

app.MapGet("/todoitems/{id}", async (int id, IMediator mediator) => await mediator.Send(new GetTodoItemByIdQuery(id)).ToOkOrNotFound());

app.MapGet("/todoitems/complete", async (IMediator mediator) => await mediator.Send(new GetCompleteTodoItemsQuery()));

app.MapPost("/todoitems", async (CreateTodoViewModel createTodoView, IMediator mediator) => await mediator.Send(new PostTodoItemCommand(createTodoView)).ToTodoOrNotFound());

app.MapPut("/todoitems/{id}", async (int id, Todo todo, IMediator mediator) => await mediator.Send(new PutTodoItemCommand(id,todo)).ToNoContentOrNotFound());

app.MapDelete("/todoitems/{id}", async (int id, IMediator mediator) => await mediator.Send(new DeleteTodoItemByIdCommand(id)).ToOkOrNotFound());


//Project API endpoints
app.MapGet("/projects", async (IMediator mediator) => await mediator.Send(new GetAllProjectsQuery()));



app.Run();

public partial class Program { }