using CorrelationTracking.Extensions;
using EUniversity.Schedule.Gateway.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();

// ****************************************
// Configure services
// ****************************************
builder.Services.AddGatewayServices(builder.Configuration);
builder.Services.AddUniversityMicroservices(builder.Configuration);
builder.Services.AddGatewaySwaggerConfiguration();
builder.Services.AddCorrelationIdTrackingServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDateOnlyTimeOnlyStringConverters();

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("GatewayPolicy",
        builder =>
        {
            builder.WithOrigins(allowedOrigins)
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCorrelationIdTrackingMiddleware();
app.UseCors("GatewayPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
