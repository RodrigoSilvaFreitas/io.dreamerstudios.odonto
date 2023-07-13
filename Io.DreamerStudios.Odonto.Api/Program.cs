using Io.DreamerStudios.Odonto.Core.Contracts;
using Io.DreamerStudios.Odonto.Core.Contracts.Gateway;
using Io.DreamerStudios.Odonto.Domain.UseCases;
using Io.DreamerStudios.Odonto.Repository.Config;
using Io.DreamerStudios.Odonto.Repository.Memory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "All", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});
builder.Services.AddDbContext<OdontoContext>(options =>
{
    options.UseMySQL(Environment.GetEnvironmentVariable("OdontoConnection") ?? "Server=localhost;DataBase=odonto;Uid=odonto;Pwd=odonto@1234");
});

builder.Services.AddScoped<IGetPersonUseCase, GetPersonUseCase>();
builder.Services.AddScoped<ICreatePersonUseCase, CreatePersonUseCase>();
builder.Services.AddScoped<IPersonGateway, PersonMemoryGateway>();

builder.Services.AddScoped<IDeletePersonUseCase, DeletePersonUseCase>();

builder.Services.AddScoped<IUpdatePersonUseCase, UpdatePersonUseCase>();

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