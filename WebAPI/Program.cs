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

app.MapPut("api/resume", async (CVContext context, Resume resume) =>
{
    try
    {
        context.Resumes.Update(resume);
        await context.SaveChangesAsync();
        return Results.Ok(resume);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
}).WithName("PutResume").WithOpenApi();

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

app.MapDelete("api/Experience/{id}", async (CVContext context, int id) =>
{
    try
    {
        var Experience = await context.Experiences.FindAsync(id);
        if (Experience == null)
        {
            return Results.NotFound();
        }

        context.Experiences.Remove(Experience);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
}).WithName("DeleteExperience").WithOpenApi();


app.MapDelete("api/project/{id}", async (CVContext context, int id) =>
{
    try
    {
        var project = await context.Projects.FindAsync(id);
        if (project == null)
        {
            return Results.NotFound();
        }

        context.Projects.Remove(project);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
}).WithName("DeleteProject").WithOpenApi();

app.MapDelete("api/education/{id}", async (CVContext context, int id) =>
{
    try
    {
        var education = await context.Educations.FindAsync(id);
        if (education == null)
        {
            return Results.NotFound();
        }

        context.Educations.Remove(education);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
}).WithName("DeleteEducation").WithOpenApi();


// app.MapGet("/secure", [Authorize] () => "Secure data")
//    .RequireAuthorization();

app.Run();

