using Microsoft.EntityFrameworkCore;
using Practica01.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//TODO: Configurar conexion
const string CONNETION_NAME = "EquiposDB";
var connection_string = builder.Configuration.GetConnectionString(CONNETION_NAME);

builder.Services.AddDbContext<EquipoContext>(options => options.UseSqlServer(connection_string)); 


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
