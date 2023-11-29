using Microsoft.AspNetCore.Builder;
namespace Core.CrossCuttingConcerns.Exceptions;
public static class ExceptionMiddlewareExtension
{
    public static void CustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
