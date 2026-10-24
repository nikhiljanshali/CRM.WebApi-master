using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CRM.Core.Infrastructure {
    public interface IActionFilter<TAttribute> where TAttribute : Attribute {
        void OnActionExecuting(TAttribute attribute, ActionExecutingContext context);
    }
}
