using AppQuiz.Api.Data;
using AppQuiz.Application.Infrastructure;
using AppQuiz.Application.Quizzes.Queries.GetById;
using AppQuiz.Application.Services;
using AppQuiz.Domain;
using AppQuiz.Persistence;
using AutoMapper;
using HealthChecks.UI.Client;
using Jaeger;
using Jaeger.Samplers;
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
using OpenTracing;
using OpenTracing.Util;
using Shared.Bus.Messages;
using Shared.Persistence.MongoDb;
using System;

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

            services.AddScoped<ICheckResultService, CheckResultsService>();

            services.AddAutoMapper(typeof(QuizProfile).Assembly);
            services.AddMediatR(typeof(GetQuizByIdQueryHandler).Assembly);

            AddMassTransit(services);

            services.AddOpenTracing();

            services.AddSingleton<ITracer>(serviceProvider =>
            {
                string serviceName = serviceProvider.GetRequiredService<IWebHostEnvironment>().ApplicationName;

                // This will log to a default localhost installation of Jaeger.
                var tracer = new Tracer.Builder(serviceName)
                    .WithSampler(new ConstSampler(true))
                    .Build();

                // Allows code that can't use DI to also access the tracer.
                GlobalTracer.Register(tracer);

                return tracer;
            });

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddMongoDb(Configuration.GetSection(ConnectionStrings.SECTION_NAME).Get<ConnectionStrings>().Mongo);

            services.AddCors(options =>
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

            var identityUrl = Configuration["IdentityUrl"];

            AppConfigureAuth(services, identityUrl);

            AppConfigureSwagger(services);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SeedData.InitializeDatabase(app).GetAwaiter().GetResult();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAll");
            UseConfigureAuth(app);
            UseConfigureSwagger(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });

                endpoints.MapHealthChecks("/readiness", new HealthCheckOptions
                {
                    Predicate = r => !r.Name.Contains("self")
                });

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

        protected virtual void AppConfigureAuth(IServiceCollection services, string identityUrl)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = identityUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = "QuizApi";
                    options.TokenValidationParameters.ValidIssuers = new[]
                    {
                        identityUrl
                    };
                }).AddCookie();


            services.AddAuthorization();
        }

        protected virtual void AddMassTransit(IServiceCollection services)
        {
            var rabbitMqHost = Configuration["RabbitMqHost"];
            services.AddMassTransit(x =>
            {
                EndpointConvention.Map<DeleteChapterMessage>(typeof(DeleteChapterMessage).GetReceiveEndpoint());
                EndpointConvention.Map<QuizResultMessage>(typeof(QuizResultMessage).GetReceiveEndpoint());
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    // configure health checks for this bus instance
                    cfg.UseHealthCheck(provider);

                    cfg.Host(rabbitMqHost);
                }));
            });

            services.AddMassTransitHostedService();
        }

        protected virtual void AppConfigureSwagger(IServiceCollection services)
        {
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
        }

        protected virtual void UseConfigureAuth(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        protected virtual void UseConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz API V1");
                c.OAuthClientId("SwaggerId");
                c.OAuthAppName("Swagger UI");
            });
        }
    }
}
