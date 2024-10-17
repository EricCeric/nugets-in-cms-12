using nugets_in_cms_12.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomLoginBackgroundImage(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoginBackgroundMiddleware>();
    }
}
