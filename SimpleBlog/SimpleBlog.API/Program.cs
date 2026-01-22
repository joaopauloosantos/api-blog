using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SimpleBlog.API.Configuration;
using SimpleBlog.API.Configuration.Ioc;
using SimpleBlog.Repository.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseInMemoryDatabase("BlogDb"));

DependencyInjection.DependencyInjectionServices(ref builder);
DependencyInjection.DependencyInjectionRepositories(ref builder);
DependencyInjection.DependencyInjectionValidations(ref builder);
DependencyInjection.DependencyInjectionNotifier(ref builder);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SimpleBlog API",
        Version = "v1",
        Description = "Desafio Técnico C#"
    });

    c.EnableAnnotations();

    // Comentários XML (Documentação dos Endpoints)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<SwaggerBasicAuthMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleBlog API v1");
    c.RoutePrefix = string.Empty; // Swagger na raiz
});

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
