using EUniversity.Schedule.Manager.Api.ExceptionHandlers;
using EUniversity.Schedule.Manager.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// *********************************
// Configure API services
// *********************************
builder.Services.ConfigureServices(builder.Configuration);

// *********************************
// Add Custom Exception Handlers
// *********************************
builder.Services.AddExceptionHandler<EntityNotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddLogging();

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
