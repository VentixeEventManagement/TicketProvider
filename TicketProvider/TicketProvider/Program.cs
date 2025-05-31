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
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExamplesFromAssemblyOf<EventExample>();

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

This microservice provides a RESTful API for event management, allowing clients to create, 
read, update, and delete event information. It follows a layered architecture pattern with
clear separation of concerns:

Architecture Overview:
- Controllers: Handle HTTP requests and responses (API endpoints)
- Business Layer: Contains business logic, services, and domain models
- Data Access Layer: Manages data persistence through repositories and entities

Key Components:
- EventController: Exposes REST endpoints for event CRUD operations
- EventService: Implements business logic for event management
- EventRepository: Handles data access operations for events
- Event: Domain model representing an event in the system

The API is documented using Swagger/OpenAPI, accessible at the root URL.
Data is persisted using Entity Framework Core with SQL Server.

For more details on available endpoints and data models, refer to the Swagger UI."
    });

    options.IncludeXmlComments(xmlPath);
    options.EnableAnnotations();
});

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

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