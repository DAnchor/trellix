using Microsoft.OpenApi.Models;
using Trellix.DataAccess.Configuration;
using Trellix.Mappers.Configuration;
using Trellix.Services.Container.Configuration;
using Trellix.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Services.AddServicesConfiguration();
builder.Services.AddMapConfiguration();
builder.Services.AddDBConfiguration(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCorsConfiguration();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
    new OpenApiInfo
    {
        Title = "Trellix API",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
