using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using PetFamily.infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer(); 

builder.Services.AddOpenApi(); 

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("VolunteerDb"));
    options.UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>());
    options.EnableSensitiveDataLogging();
});
 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/swagger/v1/swagger.json");

    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "PetFamily API v1")
    );
}


// app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();