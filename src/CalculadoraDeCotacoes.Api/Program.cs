using System.Reflection;
using CalculadoraDeCotacoes.Api.Filters;
using CalculadoraDeCotacoes.Application.Extensions;
using CalculadoraDeCotacoes.Common.HealthChecks;
using CalculadoraDeCotacoes.Common.Logging;
using CalculadoraDeCotacoes.Persistence.Context;
using CalculadoraDeCotacoes.Persistence.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;


try
{
    Log.Information("Iniciando a aplicação web");

    var builder = WebApplication.CreateBuilder(args);
    builder.AddDefaultLogging();

// Add services to the container.
    builder.Services.AddProblemDetails(options =>
        options.CustomizeProblemDetails = ctx =>
        {
            ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
            ctx.ProblemDetails.Extensions.Add("instance",
                $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
        });

    builder.Services.AddScoped<ParceiroAuthorizationFilter>();

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new GlobalExceptionFilter());
        
        options.Filters.Add<ParceiroAuthorizationFilter>();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.AddBasicHealthChecks();
    
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Calculadora de Cotações Api",
            Description = ""
        });

        var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
    });

    builder.Services.AddApplicationLayer();
    builder.Services.AddPersistenceLayer(builder.Configuration, builder.Environment.IsDevelopment());

    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Calculadora de Cotações Api V1");
        });
    }

    app.UseHttpsRedirection();

    app.UseBasicHealthChecks();

    app.MapControllers();

// When the app runs, it first creates the Database.
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplicação finalizou de maneira inesperada.");
    Console.WriteLine($"Critical error: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
}
finally
{
    Log.CloseAndFlush();
}