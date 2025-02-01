namespace Limupa.StoreLocation.Api.Middlewares
{
    public class BadWordsHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public BadWordsHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Items.TryGetValue("jsonBody", out object? jsonBody) && jsonBody is string jsonBodyString)
            {
                var badWords = new List<string> { "pis", "kaka", "kötü", "deli" };

                if (badWords.Any(word => jsonBodyString.Contains(word)))
                {
                    await ResponseBadRequest(context);
                    return;
                }
            }

            await _next(context);
        }

        private static async Task ResponseBadRequest(HttpContext context)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"message\": \"Bu gönderide hoş olmayan kelimeler var!\"}");
        }
    }
}