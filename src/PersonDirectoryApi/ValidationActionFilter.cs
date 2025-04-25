using System.Globalization;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PersonDirectoryApi;

public class ValidationActionFilter<T> : ActionFilterAttribute where T : class
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetRequiredService<IValidator<T>>();
        var model = context.ActionArguments.Values.FirstOrDefault(x => x is T) as T;

        if (model == null)
        {
            context.Result = new BadRequestObjectResult("Missing or invalid model.");
            return;
        }

        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            context.Result = new ValidationResultObject(validationResult);
            return;
        }
        
        await base.OnActionExecutionAsync(context, next);
    }
}

public class ValidationResultObject : BadRequestObjectResult
{
    public ValidationResultObject(ValidationResult validationResult) : base(validationResult.Errors
        .Select(x => new
        {
            x.PropertyName,
            x.ErrorMessage
        })
        .ToList()
    )
    {
    }
}