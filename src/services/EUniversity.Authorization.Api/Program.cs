using EUniversity.Authorization.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ****************************************
// Configure services
// ****************************************
builder.Services.AddAuthorizationServices(builder.Configuration);

// ****************************************
// Swagger settings
// ****************************************
builder.Services.AddAuthenticationServiceSwaggerConfiguration();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseAuthorizationSwaggerUI();

app.UseCors(x =>
{
    x.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
