using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CRM.Core.Infrastructure {
    public class ActionFilterDispatcher : IActionFilter {

        private readonly Func<Type, IEnumerable> container;

        public ActionFilterDispatcher(Func<Type, IEnumerable> container) {
            this.container = container;
        }

        public void OnActionExecuting(ActionExecutingContext context) {
            IEnumerable<object> attributes = context.Controller
                .GetType()
                .GetTypeInfo()
                .GetCustomAttributes(true);

            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null)
                attributes = attributes.Concat(descriptor.MethodInfo.GetCustomAttributes(true));

            foreach (var attribute in attributes) {
                Type filterType = typeof(IActionFilter<>).MakeGenericType(attribute.GetType());
                var filters = this.container.Invoke(filterType);

                foreach (dynamic actionFilter in filters)
                    actionFilter.OnActionExecuting((dynamic)attribute, context);
            }

        }

        public void OnActionExecuted(ActionExecutedContext context) { }

    }
}
