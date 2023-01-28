using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Social.Api.Models;

namespace Social.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<ErrorResponse>();

                foreach (var errorProperty in context.ModelState.Values)
                {
                    var error = new ErrorResponse();

                    foreach (var errorValue in errorProperty.Errors)
                    {
                        error.ErrorMessages.Add(errorValue.ErrorMessage);
                    }

                    errors.Add(error);
                }

                context.Result = new JsonResult(errors) { StatusCode = 400 };
            }
        }
    }
}
