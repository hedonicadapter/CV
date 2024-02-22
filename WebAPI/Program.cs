using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using DataAccess.Models;
using ConvertApiDotNet;
using ConvertApiDotNet.Exceptions;
using System.Net;
using Azure.Storage.Blobs;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CVContext>(options =>
{
    options.UseSqlServer(System.Environment.GetEnvironmentVariable("SQL_DB_CONNECTION") ?? "Data Source=../DataAccess/CV.db");
    options.EnableSensitiveDataLogging();
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("api/pdf", async () =>
{
    var httpClient = new HttpClient();
    var response = await httpClient.GetAsync("https://shfilestorage.blob.core.windows.net/cv-html/cv.html");
    return Results.Ok("Hello World");
});

app.MapPost("api/pdf", async (string html) =>
{
    try
    {
        var convertApi = new ConvertApi("LToj5AhGGVxPrfGX");
        var conversionTask = await convertApi.ConvertAsync("html", "pdf",
            new ConvertApiFileParam("https://shfilestorage.blob.core.windows.net/cv-html/cv.html")
        );
        var files = conversionTask.Files;
        Console.WriteLine(files.ToString());


        return Results.Ok(files);
    }
    catch (ConvertApiException e)
    {
        Console.WriteLine("Status Code: " + e.StatusCode);
        Console.WriteLine("Response: " + e.Response);

        if (e.StatusCode == HttpStatusCode.Unauthorized)
        {
            Console.WriteLine("Secret is not provided or no additional seconds left in the account to proceed conversion. More information https://www.convertapi.com/a;");
        }

        return Results.StatusCode((int)e.StatusCode);
    }
});

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
        Console.WriteLine(ex.Message);
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
        Console.WriteLine(ex.Message);
        return Results.BadRequest(ex.Message);
    }
}).WithName("PutResume").WithOpenApi();

app.MapPost("api/resume", async (CVContext context, Resume resume) =>
{
    Console.WriteLine("posting");
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
        Console.WriteLine(ex.Message);
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
        Console.WriteLine(ex.Message);
        return Results.BadRequest(ex.Message);
    }
}).WithName("DeleteResume").WithOpenApi();

app.MapPost("api/resume/html", async (HttpContext httpContext) =>
{
    try
    {
        var html = await httpContext.Request.ReadFromJsonAsync<string>();
        if (html == null) return Results.BadRequest("No HTML provided");

        string containerName = "cv-html";
        string blobName = "cv.html";

        BlobServiceClient blobServiceClient = new BlobServiceClient(System.Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING"));
        BlobClient blobClient = blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(html));
        await blobClient.UploadAsync(stream, overwrite: true);

        string blobUrl = blobClient.Uri.AbsoluteUri;

        return Results.Ok(blobUrl);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.BadRequest(ex.Message);
    }
}).WithName("PostResumeAsHTML").WithOpenApi();

app.MapDelete("api/experience/{id}", async (CVContext context, int id) =>
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
        Console.WriteLine(ex.Message);
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
        Console.WriteLine(ex.Message);
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
        Console.WriteLine(ex.Message);
        return Results.BadRequest(ex.Message);
    }
}).WithName("DeleteEducation").WithOpenApi();


// app.MapGet("/secure", [Authorize] () => "Secure data")
//    .RequireAuthorization();

app.Run();

