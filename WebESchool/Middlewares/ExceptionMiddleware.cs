using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebESchoolData.Exceptions;

namespace WebESchool.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(CustomException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex.ToString());
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, string message="Failed")
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                success = false,
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }));
        }
    }
}
