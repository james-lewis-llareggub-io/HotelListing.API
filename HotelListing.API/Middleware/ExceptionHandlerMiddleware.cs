using System.Net;
using System.Net.Mime;
using HotelListing.API.Models.Error;
using Newtonsoft.Json;

namespace HotelListing.API.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"EHM0001: error encountered processing {context.Request.Path}");
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        var code = HttpStatusCode.InternalServerError;
        var error = new PostError
        {
            Type = "Failure",
            Message = exception.Message
        };
        var response = JsonConvert.SerializeObject(error);
        context.Response.StatusCode = (int)code;
        await context.Response.WriteAsync(response);
    }
}