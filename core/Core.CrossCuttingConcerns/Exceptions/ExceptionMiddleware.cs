using Core.CrossCuttingConcerns.Logging;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace Core.CrossCuttingConcerns.Exceptions;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly LoggerServiceBase _logger;
    public ExceptionMiddleware(RequestDelegate next, LoggerServiceBase logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        _logger.Error(JsonConvert.SerializeObject(CreateLogData(context, exception)));
        if (exception.GetType() == typeof(ValidationException)) return CreateValidationException(context, exception);
        else if (exception.GetType() == typeof(BusinessException)) return CreateBusinessException(context, exception);
        else return CreateInternalException(context, exception);
    }
    private Task CreateBusinessException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        var businessExceptionDetails = new BusinessExceptionDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/business",
            Title = "Business exception",
            Detail = exception.Message,
            Instance = ""
        };
        var response = JsonConvert.SerializeObject(businessExceptionDetails);
        return context.Response.WriteAsync(response);
    }
    private Task CreateValidationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        var errors = ((ValidationException)exception).Errors;
        var validationExceptionDetails = new ValidationExceptionDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/validation",
            Title = "Validation error(s)",
            Detail = "",
            Instance = "",
            Errors = errors
        };
        var response = JsonConvert.SerializeObject(validationExceptionDetails);
        return context.Response.WriteAsync(response);
    }
    private Task CreateInternalException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://example.com/probs/internal",
            Title = "Internal exception",
            Detail = exception.Message,
            Instance = ""
        };
        var json = JsonConvert.SerializeObject(problemDetails);
        return context.Response.WriteAsync(json);
    }
    private LogDetail CreateLogData(HttpContext context, Exception exception)
    {
        var logParameter = new List<LogParameter>();
        logParameter.Add(new LogParameter
        {
            Type = context.Request.GetType().Name,
            Value = exception.Message
        });
        LogDetail details = new()
        {
            MethodName = context.Request.Path,
            Parameters = logParameter,
            User = context == null ||
                   context.User.Identity.Name == null
                       ? "?"
                       : context.User.Identity.Name
        };
        return details;
    }
}
