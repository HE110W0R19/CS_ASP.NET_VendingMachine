using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VendingMachine.Application.Services;
using VendingMachine.Core.Abstractions;
using VendingMachine.DataAccess;
using VendingMachine.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(g => {
    // Add documentation for each controller
    g.SwaggerDoc("User", new OpenApiInfo { Title = "User API", Version = "v1" });
});

builder.Services.AddDbContext<VendingMachineDbContext>(
    option =>
    {
        //Adding DB context
        option.UseNpgsql(builder.Configuration.GetConnectionString(nameof(VendingMachineDbContext)));
    });

//Adding objects in DI for drinks and coins services
builder.Services.AddScoped<IDrinksService, DrinksService>();
builder.Services.AddScoped<IDrinksRepository, DrinksRepository>();
builder.Services.AddScoped<ICoinService, CoinsService>();
builder.Services.AddScoped<ICoinsRepository, CoinsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s => {
        //Add swagger endpoint for user controller
        s.SwaggerEndpoint("/swagger/User/swagger.json", "User API V1");
    });
}

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseHttpsRedirection();

app.MapControllers();


//For get requests from port 3000
app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:3000");
    x.WithMethods().AllowAnyMethod();
});

app.Run();
