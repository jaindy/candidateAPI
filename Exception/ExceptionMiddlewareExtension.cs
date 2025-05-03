using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System.Net;

namespace CandidateAPI.Exception
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureBuildinExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(apperror =>
            {
                apperror.Run(async context =>

                {
                    var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    context.Response.ContentType = "application/json";

                    var errorstring = new ErrorResponseData()
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = contextFeatures.Error.Message,
                        Path = contextRequest.Path

                    }.ToString();
                    await context.Response.WriteAsync(errorstring);
                });
            });
        }
    }
}
public class ErrorResponseData
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Path { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}