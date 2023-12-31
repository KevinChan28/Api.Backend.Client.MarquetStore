using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using Api.Client.MarquetStore.Repository.Imp;
using Api.Client.MarquetStore.Repository;
using Api.Client.MarquetStore.Service;
using Api.Client.MarquetStore.Service.Imp;

namespace Api.Client.MarquetStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            const string connectionName = "MarquetStoreDB";
            var connectionString = builder.Configuration.GetConnectionString(connectionName);
            builder.Services.AddDbContext<MarquetstoreDbContext>(options => options.UseMySQL(connectionString));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiMarquetStore", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                //define security for authorization
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                 {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using bearer scheme"
                 });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                       new string[]{}
                    }

                 });
            });

            //Configuration of server
            builder.Services.AddJwtServices(builder.Configuration);
            //Roles
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("customerOnlyPolicy", policy => policy.RequireClaim("CustomerOnly", "Customer"));
                options.AddPolicy("AdministratorOrUser", policy =>
                     policy.RequireRole("Administrator", "Customer"));
            });

            builder.Services.AddTransient<IUserRepository, ImpUserRepository>();
            builder.Services.AddTransient<IUserService, ImpUserService>();
            builder.Services.AddTransient<ISaleRepository, ImpSaleRepository>();
            builder.Services.AddTransient<ISaleService, ImpSaleService>();
            builder.Services.AddTransient<IProductsRepository, ImpProductsRepository>();
            builder.Services.AddTransient<IConceptRepository, ImpConceptRepository>();
            builder.Services.AddTransient<IPersonalizationRepository, ImpPersonalizationRepository>();
            builder.Services.AddTransient<IDatabaseRepository, ImpDatabaseRepository>();

            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "Cors", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
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

            app.UseCors("Cors");

            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}