using CalculadoraDeCotacoes.Api.Filters;
using CalculadoraDeCotacoes.Application.Extensions;
using CalculadoraDeCotacoes.Persistence.Context;
using CalculadoraDeCotacoes.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProblemDetails(options =>
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
        ctx.ProblemDetails.Extensions.Add("instance",
            $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
    });

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new GlobalExceptionFilter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceLayer(builder.Configuration, builder.Environment.IsDevelopment());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

// When the app runs, it first creates the Database.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

app.Run();