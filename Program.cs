using CandidateAPI.Data;
using CandidateAPI.Exception;
using CandidateAPI.Middleware;
using CandidateAPI.Repositories.Implementation;
using CandidateAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("candidateportal", policy =>
    {
        policy.WithOrigins("http://localhost:5173/").AllowAnyMethod().AllowAnyHeader();
    });
});*/
builder.Services.AddCors();
// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

//Injecting sql server
builder.Services.AddDbContext<ApplicationDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();

var app = builder.Build();
app.ConfigureBuildinExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //  app.UseSwagger();
    //  app.UseSwaggerUI();

    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());


app.UseAuthorization();
app.UseMiddleware<ApiKeyMiddleware>();


app.MapControllers();

app.Run();
