using CorrelationTracking.Extensions;
using EUniversity.Gateway.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ****************************************
// Configure services
// ****************************************
builder.Services.AddGatewayServices(builder.Configuration);
builder.Services.AddGatewaySwaggerConfiguration();
builder.Services.AddCorrelationIdTrackingServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCorrelationIdTrackingMiddleware();
app.UseCors(x =>
{
    x.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
