using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ExchangeRate.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();

            // Add services to the container.
            builder.Services.AddControllers();


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



            var app = builder.Build();

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
    }
}