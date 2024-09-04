using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tasks.Common.Exceptions;
using FluentValidation;

namespace Tasks_BE.Middlewares
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                UserManagerException => new BadRequestObjectResult(context.Exception.Message),
                NotFoundException => new NotFoundObjectResult(context.Exception.Message),
                InvalidCredentialsException => new UnauthorizedObjectResult(context.Exception.Message),
                IncorrectParametersException => new BadRequestObjectResult(context.Exception.Message),
                AlreadyExistsException => new BadRequestObjectResult(context.Exception.Message),
                InvalidTokenException => new BadRequestObjectResult(context.Exception.Message),
                ValidationException => new BadRequestObjectResult(context.Exception.Message),
                _ => new ObjectResult(new { error = $"An unexpected error occurred: {context.Exception.Message}" })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                }
            };
        }
    }
}
