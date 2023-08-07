using Api.Contexts;
using Api.Interfaces.Repositories;
using Api.Interfaces.Services;
using Api.Repositories;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<CnabContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddDbContext<CnabContext>(options => options.UseInMemoryDatabase("teste_leandro_romanelli"));

            builder.Services.AddScoped<ICnabRepository, CnabRepository>();

            builder.Services.AddScoped<ICnabService, CnabService>(); 



            builder.Services.AddCors(c =>
            {
                c.AddPolicy("All", options => options
                   .SetIsOriginAllowed(origin => true)
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());
            });

            builder.Services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1",
                    new OpenApiInfo()
                    {
                        Title = "Teste Leandro Romanelli - Api",
                        Version = "v1"
                    });
            });

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseCors("All");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste Leandro Romanelli");
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}