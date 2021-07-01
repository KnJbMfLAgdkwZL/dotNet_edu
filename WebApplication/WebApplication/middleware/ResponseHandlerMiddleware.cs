using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApplication.middleware
{
    public class ResponseHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var responseBody = "";
            await using (var swapStream = new MemoryStream())
            {
                var originalResponseBody = context.Response.Body;
                context.Response.Body = swapStream;

                await _next(context);

                swapStream.Seek(0, SeekOrigin.Begin);
                responseBody = new StreamReader(swapStream).ReadToEnd();
                swapStream.Seek(0, SeekOrigin.Begin);
                await swapStream.CopyToAsync(originalResponseBody);
                context.Response.Body = originalResponseBody;
            }

            Console.WriteLine("Response");
            var statusCode = context.Response.StatusCode;
            Console.WriteLine($"StatusCode: {statusCode}");

            foreach (var (key, value) in context.Response.Headers)
            {
                Console.WriteLine($"\t{key}: {value}");
            }

            if (responseBody.Length > 0)
            {
                Console.WriteLine($"ResponseBody: {responseBody}");
            }

            Console.WriteLine();
        }
    }
}