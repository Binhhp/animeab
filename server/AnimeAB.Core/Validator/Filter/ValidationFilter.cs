using AnimeAB.Application.Common.ExceptionsHanlder;
using AnimeAB.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.AppAdmin.Validator.Filter
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
                var errorsInModelValue = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(key => key.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponseValidator();
                
                foreach(var error in errorsInModelValue)
                {
                    foreach(var subError in error.Value)
                    {
                        var errorModel = new ErrorValidator
                        {
                            field = error.Key,
                            message = subError
                        };

                        errorResponse.errors.Add(errorModel);
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}
