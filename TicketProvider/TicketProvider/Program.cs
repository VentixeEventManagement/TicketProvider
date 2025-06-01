// This document was formatted and refined by AI
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using TicketProvider.Business.Interfaces;
using TicketProvider.Business.Services;
using TicketProvider.Data.Contexts;
using TicketProvider.Data.Interfaces;
using TicketProvider.Data.Repositories;
using TicketProvider.SwaggerExamples;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<TicketContext>(options =>
        options.UseInMemoryDatabase("InMemoryTestDb"));
}
else
{
    builder.Services.AddDbContext<TicketContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExamplesFromAssemblyOf<TicketExample>();

// AI-generated code: Swagger XML comments configuration
var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters();
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TicketProvider API",
        Version = "v1",
        Description = @"TicketProvider Microservice

This microservice provides a RESTful API for Ticket Management"
    });

    options.IncludeXmlComments(xmlPath);
    options.EnableAnnotations();
});

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketProvider API v1");
});

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }