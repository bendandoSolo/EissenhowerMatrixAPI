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

    }
}
