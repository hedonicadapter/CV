using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);


// builder.Services.AddDbContext<AdminContext>(options =>
//         options.UseSqlite("Data Source=../DataAccess/CV.db"));

// builder.Services.AddDbContext<ResumeContext>(options =>
// {
//     options.UseSqlite("Data Source=../DataAccess/CV.db");
//     options.EnableSensitiveDataLogging();
// });

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// app.MapGet("api/resume", async (ResumeContext context) =>
// {
//     try
//     {
//         var resume = await context.Resumes
//         .Include(r => r.Educations)
//         .Include(r => r.Projects)
//         .Include(r => r.Experiences)
//         .FirstOrDefaultAsync();

//         return resume == null ? Results.NotFound() : Results.Ok(resume);
//     }
//     catch (Exception ex)
//     {
//         return Results.BadRequest(ex.Message);
//     }

// }).WithName("GetResumes").WithOpenApi();


app.MapGet("/secure", [Authorize] () => "Secure data")
   .RequireAuthorization();

app.Run();

