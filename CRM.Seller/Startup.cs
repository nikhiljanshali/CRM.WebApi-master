using CRM.Core.Infrastructure;
using CRM.DAL.BaseRepositories;
using CRM.DAL.ConfigRepositories;
using CRM.DAL.MarketSheetRepositories;
using CRM.DAL.OrganizationRepositories;
using CRM.Logging.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Seller {
    public class Startup {

        private IServiceProvider serviceProvider;

        private IEnumerable GetActionFilters(Type type) => this.serviceProvider.GetServices(type);
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<ApiBehaviorOptions>(opts => {
                opts.InvalidModelStateResponseFactory = actionContext => new BadRequestObjectResult(actionContext.ModelState);
            });

            string mongoConnectionString = Configuration["Data:DefaultConnection:MongoConnectionString"];
            string logConnectionString = Configuration["DbLog:ConnectionString"];

            //services.AddSingleton<IBaseMongoRepository>(provider => new BaseMongoRepository())
            services.AddSingleton<IMongoLogRepository>(provider => new MongoLogRepository(logConnectionString));
            services.AddSingleton<IMongoAuthLogRepository>(provider => new MongoAuthLogRepository(logConnectionString));
            services.AddSingleton<IMongoRepository>(provider => new MongoRepository(mongoConnectionString));
            services.AddScoped<IPermissionRepository, PermissionRepository>();


            services.AddScoped<IMarketSheetSellerRepository, MarketSheetSellerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IOrganizationConfigRepository, OrganizationConfigRepository>();


            services.AddOptions();

            //Enable Cors
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            // return 401 instead of redirect to login
            services.ConfigureApplicationCookie(options => {
                options.Events.OnRedirectToLogin = context => {
                    context.Response.Headers["Location"] = context.RedirectUri;
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            services.AddControllers()
                .AddNewtonsoftJson(opts => { opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); })
                .AddMvcOptions(opts => opts.Filters.Add(new ValidateModelAttribute()))
                .AddMvcOptions(opts => opts.Filters.Add(new ActionFilterDispatcher(this.GetActionFilters))) //Custom action filter
                .AddMvcOptions(opts => opts.EnableEndpointRouting = false);

            services.AddAuthentication(o => {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg => {
                    cfg.RequireHttpsMetadata = false;

                    cfg.TokenValidationParameters = new TokenValidationParameters() {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        LifetimeValidator = CustomLifetimeValidator,
                        ValidIssuer = Configuration["TokenAuthentication:Issuer"],
                        ValidAudience = Configuration["TokenAuthentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["TokenAuthentication:client_secret"]))
                    };

                    cfg.Events = new JwtBearerEvents {
                        OnAuthenticationFailed = context => {
                            Console.WriteLine("OnAuthenticationFailed: " +
                                context.Exception.Message);
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddIdentityCookies(o => { });

            services.Configure<IISOptions>(options => {
                options.ForwardClientCertificate = false;
            });

            services.AddSwaggerGen(swagger => {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Seller API With Authentication",
                    Description = "ASP.NET Core 3.1 Web API"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
        }

        private bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) {
            if (expires != null)
                return expires > DateTime.UtcNow;
            return false;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            //Service broker for actionfilter dispatcher.
            this.serviceProvider = app.ApplicationServices;

            app.UseHttpsRedirection();

            // Add MVC to the request pipeline.
            app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //Get RemoteIpAddress
            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });

            app.UseExceptionHandler(
             builder => {
                 builder.Run(
                   async context => {
                       context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                       context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                       var error = context.Features.Get<IExceptionHandlerFeature>();
                       if (error != null) {
                           context.Response.Headers.Add("Application-Error", error.Error.Message);
                           // CORS
                           context.Response.Headers.Add("access-control-expose-headers", "Application-Error");

                           await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                       }
                   });
             });

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Seller API V1");
            });

        }
    }
}
