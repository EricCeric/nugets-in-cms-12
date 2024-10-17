namespace nugets_in_cms_12.Middlewares
{
        /// <summary>
    /// Replaces the original login background image in Optimizely/Asp Net Authentication mode
    /// </summary>
    /// 
    public class LoginBackgroundMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/Util/images/login/login-background-image.png"))
            {
                context.Response.Redirect("/images/custom-login.jpg");
            }
            else
            {
                await next(context);
            }
        }
    }
}
