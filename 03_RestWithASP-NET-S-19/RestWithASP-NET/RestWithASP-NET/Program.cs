using EvolveDb;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using RestWithASP_NET.Business;
using RestWithASP_NET.Business.Implementations;
using RestWithASP_NET.Hypermedia.Enricher;
using RestWithASP_NET.Hypermedia.Filters;
using RestWithASP_NET.Model.Context;
using RestWithASP_NET.Repository;
using RestWithASP_NET.Repository.Generic;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
var appName = "REST API's From 0 to Azure with ASP.NET Core 5 and Docker";
var appVersion = "v1";
var appDescription = $"API RESTful developed in curse '{appName}'";


// Add services to the container.

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

builder.Services.AddRouting(options => options.LowercaseUrls = true);       
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = appName,
            Version = appVersion,
            Description =appDescription,
            Contact = new OpenApiContact
            {
                Name = "Leandro Costa",
                Url = new Uri("https://pub.erudio.com.br/meus-cursos")
            }
        });
});
builder.Services.AddMvc();      

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
       builder.Services.AddDbContext<MySQLContext> (options => options.UseMySql(
            connection,
            new MySqlServerVersion(new Version(8,0,36))));

        // Add Migration

       if (builder.Environment.IsDevelopment())
        {
            MigrateDatabase(connection);
        }

        builder.Services.AddMvc(options =>
                {
                    options.RespectBrowserAcceptHeader = true;

                    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
                })
            .AddXmlSerializerFormatters();

// add Hateos
       var filterOptions = new HyperMediaFilterOptions();
        filterOptions.ContetResponseEnricherList.Add(new PersonEnricher());
        filterOptions.ContetResponseEnricherList.Add(new BookEnricher());

        builder.Services.AddSingleton(filterOptions);


       // Versioning API
       builder.Services.AddApiVersioning();



       // Dependency Injection

       builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
       builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

       builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>)); ;



        var app = builder.Build();

        
        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCors();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json",
                $"{appName} - {appVersion}");
        });

        var option = new RewriteOptions();
        option.AddRedirect("^$", "swagger");
        app.UseRewriter(option);

        app.UseAuthorization();

        app.MapControllers();
        
        app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");
       
        app.Run();

void MigrateDatabase(string connection)
{
    try
    {
        var evolveConnection = new MySqlConnection(connection);
        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex);
        throw;
    }
}
