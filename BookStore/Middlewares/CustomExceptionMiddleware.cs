using BookStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore.Middlewares
{
    public class CustomExceptionMiddleware
    {
        //Log middleware , request , Response 
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService; 
        public CustomExceptionMiddleware(RequestDelegate next,ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task Invoke (HttpContext context)
        {
            var wacth = Stopwatch.StartNew();
            try
            {
               
                string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Write(message);
                await _next(context);
                wacth.Stop();


                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responed " + context.Response.StatusCode + " in " + wacth
                    .Elapsed.TotalMilliseconds + "ms";
                _loggerService.Write(message);
                //await _next(context);
            }
            catch (Exception ex)
            {
                wacth.Stop();
                await HandleException(context, ex, wacth);
            }
         
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch wacth)
        {
            context.Response.ContentType = "application/Json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "[Error] HTTP =" + context.Request.Method + "-" + context.Response.StatusCode + "Error Message" + ex.Message + "in" + wacth.Elapsed.TotalMilliseconds + "ms";
            _loggerService.Write(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
   
        }
    }
    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
