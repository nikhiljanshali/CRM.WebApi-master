using CRM.IdentityServer.Entities;
using CRM.IdentityServer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.IdentityServer.Providers {
    public class TokenProviderMiddleware {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _serializerSettings;

        public TokenProviderMiddleware(RequestDelegate next) {
            _next = next;
            _serializerSettings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
        }

        public Task Invoke(HttpContext context) {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals("/api/token", StringComparison.Ordinal)) {
                return _next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType) {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            var token = GenerateToken(context);
            return token;
        }

        private async Task GenerateToken(HttpContext context) {
            // DeSerialize and check values from request
            string deviceId = context.Request.Form["udid"];
            string client_id = context.Request.Form["client_id"];
            string userName = context.Request.Form["user_id"];
            string password = context.Request.Form["password"];

            if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password)) {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject("UserName and Password is required!", _serializerSettings));
                return;
            }

            var userManager = context.RequestServices.GetService<UserManager<ApplicationUser>>();
            var user = userManager.Users.FirstOrDefault(c => c.UserName == userName);
            if (user == null) {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject("Invalid User", _serializerSettings));
                return;
            }

            bool checkPassword = await userManager.CheckPasswordAsync(user, password);
            if (!checkPassword) {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject("Password is Invalid", _serializerSettings));
                return;
            }

            if (!string.IsNullOrEmpty(client_id.ToString()) && client_id.ToString().Equals("ngAuthApp")) 
                deviceId = context.Request.HttpContext.Connection.RemoteIpAddress.ToString();

            var repository = context.RequestServices.GetService<IRefreshTokenRepository>();
            var response = TokenGenerator.GenerateToken(user, repository, deviceId, client_id);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _serializerSettings));
        }
    }
}
