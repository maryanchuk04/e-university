using EUniversity.Gateway.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ****************************************
// Configure services
// ****************************************
builder.Services.AddGatewayServices(builder.Configuration);
builder.Services.AddGatewaySwaggerConfiguration();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
