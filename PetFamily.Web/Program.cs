using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();  

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PetFamily API",
        Version = "v1",
        Description = "Базовое API приюта. Доменный слой: Pet, Volunteer (w1-w2)."
    });
});

 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(ui =>
    {
        ui.SwaggerEndpoint("/swagger/v1/swagger.json", "PetFamily API v1");
    });
}

// app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();