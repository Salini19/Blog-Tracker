using DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

//builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
//{
//    builder.WithOrigins("https://localhost:77773", "http://localhost:65543").AllowAnyHeader().AllowAnyMethod();
//}));//2


//builder.Services.AddCors(options => options.AddPolicy(name: "AllowOrign",
//    builder =>
//    {
//        builder.WithOrigins("https://localhost:77773", "http://localhost:65543").AllowAnyHeader().AllowAnyMethod();
//    }));//3

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

//app.UseCors(builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });//2


//app.UseCors("AllowOrigin");//3

app.Run();
