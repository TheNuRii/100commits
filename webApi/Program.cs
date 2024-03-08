using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITaskService>(new InMemoryTaskService());

var app = builder.Build();

app.UseRewriter(new RewriteOptions().AddRedirect("task/(.*)", "todos/"));
app.Use(async (context, next) =>
{
    Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Started.");
    await next(context);
    Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Finished.");
});


var todos = new List<Todo>();

app.MapGet("/todos", (ITaskService service) => service.GetTodos());

app.MapGet("/todos/{id}", Results<Ok<Todo>, NotFound> (int id, ITaskService service) =>
{
    var targetTodo = service.GetTodoById(id);
    return targetTodo is null
        ? TypedResults.NotFound()
        : TypedResults.Ok(targetTodo);
});


app.MapPost("/todos", (Todo task, ITaskService service) =>
    {
        service.AddTodo(task);
        return TypedResults.Created("/todos/{id}", task);
    })
    .AddEndpointFilter(async (context, next) =>
    {
        var taskArgument = context.GetArgument<Todo>(0);
        var error = new Dictionary<string, string[]>();

        if (taskArgument.DueDate < DateTime.UtcNow)
        {
            error.Add(nameof(Todo.DueDate), ["Cannot have due date in the past"]);
        }

        if (taskArgument.IsComplited)
        {
            error.Add(nameof(Todo.IsComplited), ["Cannot add completed todo"]);
        }

        if (error.Count > 0)
        {
            return Results.ValidationProblem(error);
        }

        return await next(context);
    });

app.MapDelete("/todos/{id}", (int id, ITaskService service) =>
{
    service.DeleteTodoById(id);
    return TypedResults.NoContent();
});

app.Run();

public record Todo(int Id, string Name, DateTime DueDate, bool IsComplited);

interface ITaskService
{
    Todo? GetTodoById(int id);

    List<Todo> GetTodos();

    void DeleteTodoById(int id);

    Todo AddTodo(Todo task);
}

class InMemoryTaskService : ITaskService
{
    private readonly List<Todo> _todos = [];

    public Todo? GetTodoById(int id)
    {
        return _todos.SingleOrDefault(t => id == t.Id);
    }

    public List<Todo> GetTodos()
    {
        return _todos;
    }

    public void DeleteTodoById(int id)
    {
        _todos.RemoveAll(task => id == task.Id);
    }

    public Todo AddTodo(Todo task)
    {
        _todos.Add(task);
        return task;
    }
    
    
}
