using EUniversity.Authorization.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ****************************************
// Configure services
// ****************************************
builder.Services.AddAuthorizationServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
