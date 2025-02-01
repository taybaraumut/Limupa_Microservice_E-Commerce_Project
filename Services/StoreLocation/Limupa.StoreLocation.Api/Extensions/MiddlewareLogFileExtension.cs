using Limupa.StoreLocation.Api.Middlewares;
using System.Text;

namespace Limupa.StoreLocation.Api.Extensions
{
    public class MiddlewareLogFileExtension
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public MiddlewareLogFileExtension(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Bu kısımda isteği işle
            // Örneğin, isteği alıp loglamak istediğimiz bir nokta varsa burada işleriz
            // Örneğin, bir ekleme işlemi olduğunda loglama yapılabilir

            // Ekleme işlemi olduğunda loglama yapalım
            if (context.Request.Method == "POST" && context.Request.Path == "/api/items") // Örneğin sadece /api/items POST isteklerini logluyoruz
            {
                var requestBody = await FormatRequest(context.Request);
                var logMessage = $"[{DateTime.UtcNow}] User {context.User.Identity.Name ?? "Anonymous"} added an item: {requestBody}";

                LogToFile(logMessage); // Loglama fonksiyonunu çağırarak dosyaya yaz
            }

            await _next(context); // İşlemi devam ettir
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();

            using var reader = new StreamReader(request.Body, encoding: Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;

            return body;
        }

        private void LogToFile(string logMessage)
        {
            var filePath = @"path\to\requestLogs.txt"; // Log dosyasının yolu

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error writing to log file: {ex.Message}");
            }
        }
    }
}

