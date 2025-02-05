using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using Users.Core.Dto;

namespace Users.Api.Filters;

public class AgeValidationFilter : ActionFilterAttribute
{
    private readonly int _minAge;

    public AgeValidationFilter(int minAge)
    {
        _minAge = minAge;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("user", out var user))
        {
            if(user is UserDto model)
            {
                var today = DateTime.Today;
                var age = today.Year - model.DateOfBirth.Year;

                if(age < _minAge)
                {
                    context.Result = new BadRequestObjectResult($"User must be at least {_minAge} years old.");
                    return;
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("Invalid data model or missing DateOfBirth.");
                return;
            }
        }

        else
        {
            context.Result = new BadRequestObjectResult("DateOfBirth parameter is missing.");
            return;
        }

        base.OnActionExecuting(context);
    }
}
