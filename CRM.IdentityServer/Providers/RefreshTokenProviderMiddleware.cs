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
    public class RefreshTokenProviderMiddleware {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _serializerSettings;

        public RefreshTokenProviderMiddleware(RequestDelegate next) {
            _next = next;
            _serializerSettings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
        }

        public Task Invoke(HttpContext context) {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals("/api/refreshtoken", StringComparison.Ordinal)) {
                return _next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType) {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }
            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context) {
            var ticket = context.Request.Form["refreshToken"].ToString();
            var client_id = context.Request.Form["client_id"];

            if (string.IsNullOrWhiteSpace(ticket)) {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject("User must relogin.", _serializerSettings));
                return;
            }

            var repository = context.RequestServices.GetService<RefreshTokenRepository>();
            var refreshToken = repository.GetOne<RefreshToken>(c => c.ProtectedTicket == ticket);
            if (refreshToken == null) {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject("Refresh token not found.", _serializerSettings));
                return;
            }

            var userManager = context.RequestServices.GetService<UserManager<ApplicationUser>>();
            var user = userManager.Users.FirstOrDefault(c => c.Id == refreshToken.UserId);
            if (user == null) {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject("User not found.", _serializerSettings));
                return;
            }

            var token = TokenGenerator.GenerateToken(user, repository, refreshToken.DeviceId, client_id);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(token, _serializerSettings));
        }

    }
}
