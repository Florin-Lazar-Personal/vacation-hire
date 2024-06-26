using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using ExchangeRate.DependencyInjection;
using ExchangeRate.Providers.Mock;
using ExchangeRate.Providers.CurrencyLayer;
using ExchangeRate.Infrastructure.DependencyInjection;
using Newtonsoft.Json;
using ExchangeRate.API.Configuration;
using Serilog;

namespace ExchangeRate.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .CreateLogger();

            try
            {
                Log.Information("Starting web application");

                WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

                ExchangeRateConfiguration? configuration = builder.Configuration.Get<ExchangeRateConfiguration>();
                if (configuration is null)
                {
                    Log.Fatal("Failed to load service configuration");
                    return;
                }

                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer();

                // Add services to the container.
                builder.Services.AddControllers().AddNewtonsoftJson();


                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.ExampleFilters();

                    // Add (Auth) to action summary
                    c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "oauth2",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });


                });

                // exchange rate DI setup
                builder.Services
                                //.AddExchangeRate(p => p.AddMockProvider())
                                .AddExchangeRate(p => p.AddCurrencyLayer(o =>
                                {
                                    o.BaseUrl = configuration.CurrencyLayer.BaseUrl;
                                    o.AccessKey = configuration.CurrencyLayer.AccessKey;
                                }))
                                .AddExchangeRateInfrastructure();

                var app = builder.Build();

                // TODO: add exception middleware

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            catch (Exception exStartup)
            {
                Log.Fatal(exStartup, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
