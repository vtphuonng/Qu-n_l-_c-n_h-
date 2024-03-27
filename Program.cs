using Api1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IResidentInforManage, ResidentManagementRepository>();
builder.Services.AddScoped<IApartmentInforManage, ApartmentManagementRepository>();
builder.Services.AddScoped<IRelationship, RelationshipManagementRepository>();


builder.Services.AddDbContext<ResidentManagementDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=Cuamoniac8a5;Database=residentManagement3;"));


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

app.Run();
