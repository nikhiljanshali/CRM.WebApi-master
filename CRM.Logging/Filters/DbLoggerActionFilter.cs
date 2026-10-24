using CRM.Core.Infrastructure;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Logging.Filters {
    /// <summary>
    /// Filter for Logging via attribute
    /// </summary>
    public class DbLoggerActionFilter : IActionFilter<DbLoggerAttribute> {

        private readonly ILogger<DbLoggerActionFilter> _logger;

        public DbLoggerActionFilter(ILogger<DbLoggerActionFilter> logger) {

            this._logger = logger;
        }

        public void OnActionExecuting(DbLoggerAttribute attribute, ActionExecutingContext context) {
            //This is important to call BEFORE invoking action to be able to read the body again afterwards
            context.HttpContext.Request.EnableBuffering();

            var currentUser = context.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userIpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            string controller = context.RouteData?.Values["controller"].ToString();
            string action = context.RouteData?.Values["action"].ToString();

            //Define only if you not fount object from request through query param.

            List<object> data = new List<object>();

            if (attribute.LogDataType != LogDataType.Empty) {
                /* Not required due to implemention in filter
                if (attribute.LogDataType == LogDataType.Param)
                    data = context.HttpContext.Request.Query[attribute.QueryParam];
                else
                */
                string[] logParams = attribute.QueryParam.Split(',').ToArray();
                foreach (var param in logParams)
                    data.Add(context.ActionArguments[param]);
            }

            //var data = context.HttpContext.Request.Query[attribute.QueryParam];
            var logObj = new LogObject(currentUser, userIpAddress, controller, action, attribute.Description);

            if (data.Count > 0)
                _logger.LogInformation("{LogObject} {PayLoad}", logObj, data);
            else
                _logger.LogInformation("{LogObject}", logObj);
        }

        private string ReadBodyAsString(HttpRequest request) {
            var initialBody = request.Body;

            try {
                request.EnableBuffering();

                using (var reader = new StreamReader(request.Body)) {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
            finally {
                // Workaround so MVC action will be able to read body as well
                request.Body = initialBody;
            }
        }

        private static async Task<string> GetRequestBodyAsync(HttpContext context) {
            var req = context.Request;
            req.Body.Position = 0; //Rewind to initial position
                                   // The `leaveOpen` should be `true` if there's another middleware using this after or if the action going to be invoked AFTER this middleware
            using (var reader = new StreamReader(req.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, leaveOpen: true)) {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
