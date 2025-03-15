using Microsoft.EntityFrameworkCore;
using TrainComponentManager.API.Data;
using TrainComponentManager.API.Interfaces;
using TrainComponentManager.API.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext configuration
builder.Services.AddDbContext<ComponentsDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddScoped<IComponentRepository, ComponentRepository>();

var clientUrl = builder.Configuration.GetValue<string>("ClientUrl");
if (clientUrl != null)
{
    builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(clientUrl)
        .AllowAnyHeader()
        .AllowAnyMethod();
    }));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
