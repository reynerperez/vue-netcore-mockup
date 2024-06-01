using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace Presentation.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }

            catch (Exception e)
            {
                Log.Error(e, e.Message);
                if (e is ValidationException fluentException)
                {
                    var problemDetails = new ProblemDetails();
                    problemDetails.Instance = context.Request.Path;

                    problemDetails.Title = "one or more validation errors occurred.";
                    problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    List<string> validationErrors = new List<string>();
                    foreach (var error in fluentException.Errors)
                    {
                        validationErrors.Add(error.ErrorMessage);
                    }
                    problemDetails.Extensions.Add("errors", validationErrors);
                    problemDetails.Status = context.Response.StatusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(problemDetails);

                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    ProblemDetails problem = new()
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = "Server Error",
                        Title = "Server Error",
                        Detail = "An internal server has ocurred."
                    };

                    string json = JsonSerializer.Serialize(problem);

                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(json);
                }


            }
        }
    }
}
