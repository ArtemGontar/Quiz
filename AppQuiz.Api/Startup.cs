using AppQuiz.Api.Filters;
using AppQuiz.Application.Infrastructure;
using AppQuiz.Application.Quizzes.Queries.GetById;
using AppQuiz.Domain;
using AppQuiz.Persistence;
using AppQuiz.Persistence.Abstractions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace AppQuiz.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<QuizDbContext>();
            //services.AddScoped<IRepository<Quiz>, QuizRepository>();
            //services.AddScoped<IRepository<Question>, QuestionRepository>();
            //services.AddAutoMapper(typeof(QuizProfile).Assembly);
            //services.AddMediatR(typeof(GetQuizByIdQueryHandler).Assembly);

            var identityUrl = Configuration["IdentityUrl"];

            services.AddAuthentication(config =>
                {
                    config.DefaultScheme = "Cookie";
                    config.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookie")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.ClientId = "my_client_id";
                    options.ClientSecret = "my_client_id_secret";
                    options.SaveTokens = true;
                    options.RequireHttpsMetadata = false;
                    options.Authority = identityUrl;
                    options.ResponseType = "code";
                });
            
            //services.AddAuthorization();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo()
            //    {
            //        Title = "You api title",
            //        Version = "v1"
            //    });
            //    c.OperationFilter<AuthorizeCheckOperationFilter>();
            //    c.AddSecurityDefinition("oauth2",
            //        new OpenApiSecurityScheme()
            //        {
            //            Type = SecuritySchemeType.OAuth2,
            //            Flows = new OpenApiOAuthFlows()
            //            {
            //                Implicit = new OpenApiOAuthFlow()
            //                {
            //                    AuthorizationUrl = new Uri($"{identityUrl}/connect/authorize"),
            //                    TokenUrl = new Uri($"{identityUrl}/connect/token"),
            //                    Scopes = new Dictionary<string, string>
            //                    {
            //                        {"QuizApi", "Quiz API - full access"}
            //                    },
            //                }
            //            }
            //        });
            //});

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseCors(builder => builder
            //    .SetIsOriginAllowed((host) => true)
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials()
            //    .WithExposedHeaders("Content-Disposition"));

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz API V1");
            //    c.OAuthClientId("SwaggerId");
            //    c.OAuthAppName("Swagger UI");
            //});

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        protected virtual void ConfigureServicesAuth(IServiceCollection services)
        {
            var identityUrl = Configuration["IdentityUrl"];


            services.AddAuthentication("Bearer")
                .AddOAuth("Bearer", options =>
                {
                    options.AuthorizationEndpoint = identityUrl;
                    options.ClientId = "QuizApi";

                })
                .AddCookie();

            services.AddAuthorization();

        }
    }
}
