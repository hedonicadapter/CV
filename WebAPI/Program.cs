using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CVContext>(options =>
{
    options.UseSqlite("Data Source=../DataAccess/CV.db");
    options.EnableSensitiveDataLogging();
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.
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


app.MapGet("api/resume", async (CVContext context) =>
{
    try
    {
        var resume = await context.Resumes
        .Include(r => r.Educations)
        .Include(r => r.Projects)
        .Include(r => r.Experiences)
        .FirstOrDefaultAsync();

        return resume == null ? Results.NotFound() : Results.Ok(resume);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }

}).WithName("GetResume").WithOpenApi();

app.MapPost("api/resume", async (CVContext context, Resume resume) =>
{
    try
    {
        if (context.Resumes.Any(r => r.Id == resume.Id))
        {
            context.Resumes.Update(resume);
        }
        else
        {
            context.Resumes.Add(resume);
        }

        await context.SaveChangesAsync();
        return Results.Created($"/api/resume/{resume.Id}", resume);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
}).WithName("PostResume").WithOpenApi();

app.MapDelete("api/resume/{id}", async (CVContext context, int id) =>
{
    try
    {
        var resume = await context.Resumes.FindAsync(id);
        if (resume == null)
        {
            return Results.NotFound();
        }

        context.Resumes.Remove(resume);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
}).WithName("DeleteResume").WithOpenApi();


// app.MapGet("/secure", [Authorize] () => "Secure data")
//    .RequireAuthorization();

app.Run();

