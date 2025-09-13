using EissenhowerMatrixBackend.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EissenhowerMatrixBackend.Extensions
{
    public static class ResultsExtensions
    {
        public static IResult ToOkOrNotFound<T>(this T result)
        {
            if (result == null)
                return Results.NotFound();
            return Results.Ok(result);
        }

        public static async Task<IResult> ToOkOrNotFound<T>(this Task<T> task)
        {
            var result = await task;
            return ToOkOrNotFound(result);
        }

        public static IResult ToTodoOrNotFound(this Todo? result)
        {
            if (result == null)
                return Results.NotFound();
            return Results.Created($"/todoitems/{result.Id}", result);
        }

        public static async Task<IResult> ToTodoOrNotFound(this Task<Todo?> task)
        {
            var result = await task;
            return ToTodoOrNotFound(result);
        }

        public static IResult ToNoContentOrNotFound(this Todo? result)
        {
            if (result == null)
                return Results.NotFound();
            return Results.NoContent();
        }

        public static async Task<IResult> ToNoContentOrNotFound(this Task<Todo?> task)
        {
            var result = await task;
            return ToNoContentOrNotFound(result);
        }

    }
}
