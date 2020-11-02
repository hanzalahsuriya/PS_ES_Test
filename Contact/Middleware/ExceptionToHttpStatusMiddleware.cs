using System;
using System.Net;
using System.Threading.Tasks;
using Contact.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Contact.Middleware
{
    public class ExceptionToHttpStatusMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionToHttpStatusMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string error;
            switch (exception)
            {
                
                case ContactNotFoundException contactNotFoundException:
                    code = HttpStatusCode.BadRequest;
                    error = contactNotFoundException.Message;
                    break;
                case ContactAlreadyExistsException contactAlreadyExistsException:
                    code = HttpStatusCode.BadRequest;
                    error = contactAlreadyExistsException.Message;
                    break;
                case ContactDomainException contactException:
                    code = HttpStatusCode.BadRequest;
                    error = contactException.Message;
                    break;
                default:
                    error = "exception";
                    break;
            }

            var errorModel = new ErrorModel()
            {
                ErrorMessage = error
            };

            var errorJson = System.Text.Json.JsonSerializer.Serialize(errorModel);

            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(errorJson);
        }
    }
}
