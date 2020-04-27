using System;
using AppQuiz.Application.Infrastructure;
using AppQuiz.Application.Quizzes.Queries.GetById;
using AppQuiz.Domain;
using AppQuiz.Persistence;
using AutoMapper;
using GreenPipes;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shared.Persistence.MongoDb;

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
            services.Configure<ConnectionStrings>(Configuration.GetSection(ConnectionStrings.SECTION_NAME));
            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(2);
                options.Predicate = (check) => check.Tags.Contains("ready");
            });

            services.AddSingleton<QuizDbContext>();
            services.AddScoped<IRepository<Chapter>, ChapterRepository>();
            services.AddScoped<IRepository<Quiz>, QuizRepository>();
            services.AddScoped<IRepository<Question>, QuestionRepository>();
            
            services.AddAutoMapper(typeof(QuizProfile).Assembly);
            services.AddMediatR(typeof(GetQuizByIdQueryHandler).Assembly);
            services.AddMassTransit(x =>
            {

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    // configure health checks for this bus instance
                    cfg.UseHealthCheck(provider);

                    cfg.Host("rabbitmq://localhost");

                    cfg.ReceiveEndpoint("delete-chapter", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                    });
                }));
            });
            services.AddMassTransitHostedService();

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
                    options.ClientSecret = "my_client_secret";
                    options.SaveTokens = true;
                    options.RequireHttpsMetadata = false;
                    options.Authority = identityUrl;
                    options.ResponseType = "code";
                });


            //services.AddAuthorization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "You api title",
                    Version = "v1"
                });
                //c.OperationFilter<AuthorizeCheckOperationFilter>();
                //c.AddSecurityDefinition("oauth2",
                //    new OpenApiSecurityScheme()
                //    {
                //        Type = SecuritySchemeType.OAuth2,
                //        Flows = new OpenApiOAuthFlows()
                //        {
                //            Implicit = new OpenApiOAuthFlow()
                //            {
                //                AuthorizationUrl = new Uri($"{identityUrl}/connect/authorize"),
                //                TokenUrl = new Uri($"{identityUrl}/connect/token"),
                //                Scopes = new Dictionary<string, string>
                //                {
                //                    {"QuizApi", "Quiz API - full access"}
                //                },
                //            }
                //        }
                //    });
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz API V1");
                c.OAuthClientId("SwaggerId");
                c.OAuthAppName("Swagger UI");
            });

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                // The readiness check uses all registered checks with the 'ready' tag.
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("ready"),
                });

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
                {
                    // Exclude all checks and return a 200-Ok.
                    Predicate = (_) => false
                });
            });
        }
    }
}
