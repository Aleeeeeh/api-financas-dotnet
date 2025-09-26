using AFSilva.Core.Auth;
using ApiFinancas.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddDependencyInjection(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthConfiguration();

app.UseCors("AllowSpecificOrigins");

app.MapControllers();

app.Run();
